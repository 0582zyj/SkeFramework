using CSRedis;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities.Constants;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.DataUtils;
using SkeFramework.Core.WebSocketPush.PushServices.PushBrokers;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushServer
{
    public delegate void SessionHandler<TAppSession, TParam>(TAppSession session, TParam value) where TAppSession : WebSocketSession;
    /// <summary>
    /// 服务端核心类实现
    /// </summary>
    public class WebSocketServerBroker : WebSocketBorker, IPushBroker
    {
        #region 服务端事件定义
        /// <summary>
        /// 新消息到达
        /// </summary>
        public event SessionHandler<WebSocketSession,string> NewMessageReceived;
        public event SessionHandler<WebSocketSession, Guid> NewSessionConnected;
        #endregion

        /// <summary>
        /// 消息缓冲区大小
        /// </summary>
        public const int BufferSize = 4096;
        /// <summary>
        /// 订阅客户端
        /// </summary>
        protected WebSocketChannelClient ChannelClient = null;
        /// <summary>
        /// 服务端基础路径
        /// </summary>
        protected string _server { get; set; }
        /// <summary>
        /// 集群服务端的客户端列表
        /// </summary>
        private ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, WebSocketSession>> ClusterServer;

        public WebSocketServerBroker(WebSocketServerConfig options) : base(options)
        {
            ClusterServer = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, WebSocketSession>>();
            ChannelClient = new WebSocketChannelClient(options);
            _server = options.ServerPath;
            var ServerKey = RedisKeyFormatUtil.GetServerKey(_appId, _server);
            var OnLineServerKey = RedisKeyFormatUtil.GetOnLineServerKey(_appId);
            _redis.HSet(OnLineServerKey, _appId, _server);
            _redis.Subscribe((ServerKey, RedisSubScribleMessage));
        }

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task Acceptor(HttpContext context, Func<Task> next)
        {
            if (!context.WebSockets.IsWebSocketRequest) return;
            Guid SessionId = NewSessionTokenVerify( context);
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var session = new WebSocketSession(socket, SessionId);
            this.NewSessionConnectedHandle(session);
            try
            {
                while (socket.State == WebSocketState.Open && ClusterServer.ContainsKey(session.SessionId))
                {//开始接受客户端消息
                    await ReceiveMessageHandle(context, session);
                }
                socket.Abort();
            }
            catch
            {
            }
            this.SessionClosedHandle(session);
        }
        /// <summary>
        /// 新链接到达校验
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected Guid NewSessionTokenVerify(HttpContext context)
        {
            string token = context.Request.Query["token"];
            if (string.IsNullOrEmpty(token))
                throw new WebSocketException((int)WebSocketErrorCodeType.TokenExpired, WebSocketErrorCodeType.TokenExpired.ToString());
            var tokenRedisKey = RedisKeyFormatUtil.GetConnectToken(this._appId, token);
            var token_value = _redis.Get(tokenRedisKey);
            if (string.IsNullOrEmpty(token_value))
                throw new WebSocketException((int)WebSocketErrorCodeType.TokenExpired, WebSocketErrorCodeType.TokenExpired.ToString());
            var data = JsonConvert.DeserializeObject<TokenValue>(token_value);
            return data.SessionId;
        }
        /// <summary>
        /// 新链接到达
        /// </summary>
        /// <param name="session"></param>
        protected void NewSessionConnectedHandle(WebSocketSession session)
        {
            var newSessionId = Guid.NewGuid();
            var wslist = ClusterServer.GetOrAdd(session.SessionId, cliid => new ConcurrentDictionary<Guid, WebSocketSession>());
            wslist.TryAdd(newSessionId, session);
            //发布一个上线通知
            _redis.StartPipe(a => a.HIncrBy(OnlineKey, session.SessionId.ToString(), 1)
            .Publish(this.OnlineEventKey, session.SessionId.ToString()));
            if (NewSessionConnected != null)
            {
                NewSessionConnected(session, newSessionId);
            }
        }
        /// <summary>
        /// 端口关闭处理
        /// </summary>
        /// <param name="clientId"></param>
        protected void SessionClosedHandle(WebSocketSession session)
        {
            var clientId = session.SessionId;
            var wslist = ClusterServer.GetOrAdd(clientId, cliid => new ConcurrentDictionary<Guid, WebSocketSession>());
            wslist.TryRemove(clientId, out var oldcli);
            if (wslist.Count == 0)
                ClusterServer.TryRemove(clientId, out var oldwslist);
            _redis.Eval($"if redis.call('HINCRBY', KEYS[1], '{clientId}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{clientId}') end return 1",
                OnlineKey);
           ChannelClient.UnSubscribeChannel(clientId, ChannelClient.GetChanListByClientId(clientId));
            //发布一个下线通知
             _redis.Publish(OfflineEventKey, clientId.ToString());
        }
        /// <summary>
        /// 接受消息处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task ReceiveMessageHandle(HttpContext context, WebSocketSession session)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await session.SocketClient.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                if (NewMessageReceived != null)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    NewMessageReceived(session, message);
                }
                result = await session.SocketClient.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await session.SocketClient.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        /// <summary>
        /// 消息订阅处理
        /// </summary>
        /// <param name="e"></param>
        public void RedisSubScribleMessage(CSRedis.CSRedisClient.SubscribeMessageEventArgs e)
        {
            try
            {
                Trace.WriteLine($"收到消息：{e.Body}");
                var data = JsonConvert.DeserializeObject<WebSocketNotifications>(e.Body);
                var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes(data.Message));
                foreach (var clientId in data.ReceiveClientId)
                {
                    if (ClusterServer.TryGetValue(clientId, out var wslist) == false)
                    {
                        Trace.WriteLine($"websocket{clientId}离线了，{data.Message}" + (data.Receipt ? "[消息回调]" : ""));
                        if (data.CheckReceipt(clientId))
                        {
                            string message = new NotificationsVo(NotificationsType.receipt_offline, data.Message).ToString();
                            SendMessage(clientId, new[] { data.SenderClientId }, message);
                        }
                        continue;
                    }

                    ICollection<WebSocketSession> sockarray = wslist.Values;
                    //如果接收消息人是发送者，并且接收者只有1个以下，则不发送
                    //只有接收者为多端时，才转发消息通知其他端
                    if (clientId == data.SenderClientId && sockarray.Count <= 1) continue;
                    //发送WebSocket
                    foreach (var sh in sockarray)
                    {
                        sh.SocketClient.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    if (data.CheckReceipt(clientId))
                    {
                        string message = new NotificationsVo(NotificationsType.receipt_send, data.Message).ToString();
                        SendMessage(clientId, new[] { data.SenderClientId },message);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"订阅方法出错了：{ex.Message}");
            }
        }
        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="receiveClientId">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        public void SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, string message, bool receipt = false)
        {
            ((IPushBroker)ChannelClient).SendMessage(senderClientId, receiveClientId, message, receipt);
        }
    }
}
