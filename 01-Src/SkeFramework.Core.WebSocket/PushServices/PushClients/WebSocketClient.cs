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
        /// 构造函数 
        /// </summary>
        /// <param name="configs"></param>
        public WebSocketClient(WebSocketClientConfig configs)
            :base(configs)
        {
        }

        #region 连接管理
        /// <summary>
        /// 连接前的负载、授权，返回 ws 目标地址，
        /// 使用该地址连接 websocket 服务端
        /// </summary>
        /// <param name="SessionId">客户端id</param>
        /// <param name="clientExtraProps">客户端相关信息，比如ip</param>
        /// <returns>websocket 地址：ws://xxxx/ws?token=xxx</returns>
        public string PrevConnectServer(Guid SessionId, string clientExtraProps)
        {
            var server = SelectServer(SessionId);
            var token = TokenUtil.GeneratorToken();
            TokenValue tokenValue = new TokenValue()
            {
                SessionId = SessionId,
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
        /// <param name="SessionId"></param>
        /// <returns></returns>
        public bool HasOnline(Guid SessionId)
        {
            return _redis.HGet<int>(OnlineKey, SessionId.ToString()) > 0;
        }
        #endregion

        #region 消息管理
        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderSessionId">发送者的客户端id</param>
        /// <param name="receiveSessionIds">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        public void SendMessage(Guid senderSessionId, IEnumerable<Guid> receiveSessionIds, string message, bool receipt = false)
        {
            receiveSessionIds = receiveSessionIds.Distinct().ToArray();
            var Notifications = new WebSocketNotifications()
            {
                SenderSessionId = senderSessionId,
                ReceiveSessionIds = receiveSessionIds.ToList(),
                Message = message,
                Receipt = receipt,
            };
            this.SendMessage(Notifications);
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
