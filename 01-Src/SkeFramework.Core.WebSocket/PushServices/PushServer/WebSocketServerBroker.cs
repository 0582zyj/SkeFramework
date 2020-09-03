using CSRedis;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.DataUtils;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushServer
{
    /// <summary>
    /// 服务端核心类实现
    /// </summary>
    public class WebSocketServerBroker: WebSocketSession
    {
        protected string _server { get; set; }

        public WebSocketServerBroker(WebSocketServerOptions options) : base(options)
        {
            _server = options.ServerName;
            _redis.Subscribe(($"{_redisPrefix}Server{_server}", RedisSubScribleMessage));
        }

        const int BufferSize = 4096;
        ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, WebSocketServerClient>> _clients = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, WebSocketServerClient>>();


        public async Task Acceptor(HttpContext context, Func<Task> next)
        {
            if (!context.WebSockets.IsWebSocketRequest) return;

            string token = context.Request.Query["token"];
            if (string.IsNullOrEmpty(token)) return;
            var tokenRedisKey = RedisKeyFormatUtil.GetConnectToken(this.appId, token);
            var token_value = _redis.Get(tokenRedisKey);
            if (string.IsNullOrEmpty(token_value))
                throw new Exception("授权错误：Token已过期，请重新获取");

            var data = JsonConvert.DeserializeObject<TokenValue>(token_value);
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var cli = new WebSocketServerClient(socket, data.clientId);
            var newid = Guid.NewGuid();
            var wslist = _clients.GetOrAdd(data.clientId, cliid => new ConcurrentDictionary<Guid, WebSocketServerClient>());
            wslist.TryAdd(newid, cli);
            //发布一个上线通知
            _redis.StartPipe(a => a.HIncrBy(onlineKey, data.clientId.ToString(), 1)
            .Publish(this.OnlineEventKey, token_value));

            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);
            try
            {
                while (socket.State == WebSocketState.Open && _clients.ContainsKey(data.clientId))
                {
                    var incoming = await socket.ReceiveAsync(seg, CancellationToken.None);
                    var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                }
                socket.Abort();
            }
            catch
            {
            }
            wslist.TryRemove(newid, out var oldcli);
            if (wslist.Count== 0)
                _clients.TryRemove(data.clientId, out var oldwslist);
            _redis.Eval($"if redis.call('HINCRBY', KEYS[1], '{data.clientId}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{data.clientId}') end return 1",
                onlineKey);
            LeaveChan(data.clientId, GetChanListByClientId(data.clientId));
            //发布一个下线通知
            _redis.Publish(OfflineEventKey, token_value);
        }

        /// <summary>
        /// 消息订阅处理
        /// </summary>
        /// <param name="e"></param>
        public void RedisSubScribleMessage(CSRedis.CSRedisClient.SubscribeMessageEventArgs e)
        {
            try
            {
                Trace.WriteLine($"收到消息：{e.Body}" );
                var data = JsonConvert.DeserializeObject<WebSocketNotifications>(e.Body);
                var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes(data.Message));
                foreach (var clientId in data.ReceiveClientId)
                {
                    if (_clients.TryGetValue(clientId, out var wslist) == false)
                    {
                        Trace.WriteLine($"websocket{clientId}离线了，{data.Message}" + (data.Receipt ? "[消息回调]" : ""));
                        if (data.CheckReceipt(clientId))
                        {
                            SendMessage(clientId, new[] { data.SenderClientId }, new
                            {
                                data.Message,
                                receipt = "offline"
                            });
                        }
                        continue;
                    }

                    ICollection<WebSocketServerClient> sockarray = wslist.Values;
                    //如果接收消息人是发送者，并且接收者只有1个以下，则不发送
                    //只有接收者为多端时，才转发消息通知其他端
                    if (clientId == data.SenderClientId && sockarray.Count <= 1) continue;
                    //发送WebSocket
                    foreach (var sh in sockarray)
                    {
                        sh.socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    if (data.CheckReceipt(clientId))
                    {
                        SendMessage(clientId, new[] { data.SenderClientId }, new
                        {
                            data.Message,
                            receipt = "SendSuccess"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"订阅方法出错了：{ex.Message}");
            }
        }
    }
}
