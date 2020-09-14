using CSRedis;
using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities;
using SkeFramework.Core.WebSocketPush.DataEntities.Constants;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.DataUtils;
using SkeFramework.Core.WebSocketPush.PushServices.PushBrokers;
using SkeFramework.Core.WebSocketPush.PushServices.PushEvent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushClients
{
    /// <summary>
    /// WebSocket客户端连接
    /// </summary>
    public class WebSocketClient: WebSocketBorker, IPushBroker
    {
        /// <summary>
        /// 推送消息的事件，可审查推向哪个Server节点
        /// </summary>
        public EventHandler<NotificationsEventArgs> OnSend;

        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="configs"></param>
        public WebSocketClient(WebSocketClientConfig configs)
            :base(configs)
        {
        }
       
        #region 连接管理
        /// <summary>
        /// 负载分区规则：取clientId后四位字符，
        /// 转成10进制数字0-65535，求模
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        protected string SelectServer(Guid clientId)
        {
            string OnLineServerKey = RedisKeyFormatUtil.GetOnLineServerKey(_appId);
            var ret = _redis.HGetAll<string>(OnLineServerKey);
            if(ret == null || ret.Count == 0)
            {
                throw new WebSocketException((int)WebSocketErrorCodeType.ServiceNotStart, WebSocketErrorCodeType.ServiceNotStart.ToString());
            }
            _servers= ret.Values.ToList();
            var servers_idx = int.Parse(clientId.ToString("N").Substring(28), NumberStyles.HexNumber) % _servers.Count;
            if (servers_idx >= _servers.Count) servers_idx = 0;
            return _servers[servers_idx];
        }
        /// <summary>
        /// 连接前的负载、授权，返回 ws 目标地址，
        /// 使用该地址连接 websocket 服务端
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="clientExtraProps">客户端相关信息，比如ip</param>
        /// <returns>websocket 地址：ws://xxxx/ws?token=xxx</returns>
        public string PrevConnectServer(Guid clientId, string clientExtraProps)
        {
            var server = SelectServer(clientId);
            var token = TokenUtil.GeneratorToken();
            TokenValue tokenValue = new TokenValue()
            {
                SessionId = clientId,
                clientExtraProps = clientExtraProps
            };
            var tokenRedisKey = RedisKeyFormatUtil.GetConnectToken(this._appId, token);
            _redis.Set(tokenRedisKey, JsonConvert.SerializeObject(tokenValue), ConstData.TokenExpireTime);
            return $"ws://{server}{_pathMatch}?token={token}";
        }
        #endregion

        #region 在线检查

        /// <summary>
        /// 获取所在线客户端id
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Guid> GetClientListByOnline()
        {
            return _redis.HKeys(OnlineKey).Select(a => Guid.TryParse(a, out var tryguid) ? tryguid : Guid.Empty).Where(a => a != Guid.Empty);
        }
        /// <summary>
        /// 判断客户端是否在线
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool HasOnline(Guid clientId)
        {
            return _redis.HGet<int>(OnlineKey, clientId.ToString()) > 0;
        }
        #endregion

        #region 消息管理
        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="receiveClientId">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        public void SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, string message, bool receipt = false)
        {
            receiveClientId = receiveClientId.Distinct().ToArray();
            Dictionary<string, NotificationsEventArgs> redata = new Dictionary<string, NotificationsEventArgs>();
            var Notifications = new WebSocketNotifications()
            {
                SenderClientId = senderClientId,
                ReceiveClientId = receiveClientId.ToList(),
                Message = message,
                Receipt = receipt
            };
            foreach (var uid in receiveClientId)
            {
                string server = SelectServer(uid);
                if (redata.ContainsKey(server) == false)
                    redata.Add(server, new NotificationsEventArgs(server, Notifications));
                redata[server].AddReceiveClientId(uid);
            }
            foreach (var sendArgs in redata.Values)
            {
                OnSend?.Invoke(this, sendArgs);
                var ServerKey = RedisKeyFormatUtil.GetServerKey(_appId, sendArgs.Server);
                _redis.Publish(ServerKey,JsonConvert.SerializeObject(Notifications));
            }
        }
        /// <summary>
        /// 事件订阅
        /// </summary>
        /// <param name="online">上线</param>
        /// <param name="offline">下线</param>
        public void EventSubscribe( Action<TokenValue> online,Action<TokenValue> offline)
        {
            _redis.Subscribe(
                (OnlineEventKey, msg => online(JsonConvert.DeserializeObject<TokenValue>(msg.Body))),
                (OfflineEventKey, msg => offline(JsonConvert.DeserializeObject<TokenValue>(msg.Body))));
        }
        #endregion
    }
}
