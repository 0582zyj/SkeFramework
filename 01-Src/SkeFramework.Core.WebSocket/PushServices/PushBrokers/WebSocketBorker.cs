using CSRedis;
using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities;
using SkeFramework.Core.WebSocketPush.DataEntities.Constants;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.DataUtils;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using SkeFramework.Core.WebSocketPush.PushServices.PushEvent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushBrokers
{
    /// <summary>
    /// 推送核心配置
    /// </summary>
    public class WebSocketBorker
    {
        #region 事件处理程序
        /// <summary>
        /// 推送消息的事件，可审查推向哪个Server节点
        /// </summary>
        public EventHandler<NotificationsEventArgs> OnSend;
        /// <summary>
        /// 服务端处理事件
        /// </summary>
        public EventHandler<NotificationsEventArgs> OnServerHandler;
        #endregion

        /// <summary>
        /// Redis客户端
        /// </summary>
        protected CSRedisClient _redis;
        /// <summary>
        /// 集群服务端列表
        /// </summary>
        protected List<string> _servers;
        /// <summary>
        /// 应用ID 
        /// </summary>
        protected string _appId;
        /// <summary>
        /// WS-Path
        /// </summary>
        public string _pathMatch;

        #region RedisKey
        /// <summary>
        /// 在线Redis的RedisKey
        /// </summary>
        protected string OnlineKey { get { return RedisKeyFormatUtil.GetOnLineClientKey(_appId); } }
        /// <summary>
        /// 上线事件的RedisKey
        /// </summary>
        protected string OnlineEventKey
        {
            get
            {
                return RedisKeyFormatUtil.GetPublishChannelKey(_appId, RedisKey.ws_event_online);
            }
        }
        /// <summary>
        /// 下线事件的RedisKey
        /// </summary>
        protected string OfflineEventKey
        {
            get
            {
                return RedisKeyFormatUtil.GetPublishChannelKey(_appId, RedisKey.ws_event_offline);
            }
        }

        /// <summary>
        /// 订阅列表的RedisKey
        /// </summary>
        protected string ChannelListKey
        {
            get
            {
                return RedisKeyFormatUtil.GetChannelListKey(_appId);
            }
        }
        #endregion

        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="configs"></param>
        public WebSocketBorker(WebSocketClientConfig configs)
        {
            if (configs.Redis == null) throw new ArgumentException("链接参数错误", "Redis"); 
            if (String.IsNullOrEmpty(configs.PathMatch)) throw new ArgumentException("参数不能为空", "PathMatch");
            _redis = configs.Redis;
            _pathMatch =String.IsNullOrEmpty(configs.PathMatch) ? "/ws":configs.PathMatch.Insert(0,@"/");
            _appId = configs.PathMatch.Trim('/').Replace('/', '_');
        }

        #region 连接管理
        /// <summary>
        /// 负载分区规则：取SessionId后四位字符，
        /// 转成10进制数字0-65535，求模
        /// </summary>
        /// <param name="SessionId">客户端id</param>
        /// <returns></returns>
        protected string SelectServer(Guid SessionId)
        {
            string OnLineServerKey = RedisKeyFormatUtil.GetOnLineServerKey(_appId);
            var ret = _redis.HGetAll<string>(OnLineServerKey);
            if (ret == null || ret.Count == 0)
            {
                throw new WebSocketException((int)WebSocketErrorCodeType.ServiceNotStart, WebSocketErrorCodeType.ServiceNotStart.ToString());
            }
            _servers = ret.Values.ToList();
            var servers_idx = int.Parse(SessionId.ToString("N").Substring(28), NumberStyles.HexNumber) % _servers.Count;
            if (servers_idx >= _servers.Count) servers_idx = 0;
            return _servers[servers_idx];
        }
        #endregion

        #region 发送消息
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="notifications"></param>
        public void SendMessage(WebSocketNotifications notifications)
        {
            Dictionary<string, NotificationsEventArgs> redata = new Dictionary<string, NotificationsEventArgs>();
            foreach (var uid in notifications.ReceiveSessionIds)
            {
                string server = SelectServer(uid);
                if (redata.ContainsKey(server) == false)
                    redata.Add(server, new NotificationsEventArgs(server, notifications));
                redata[server].AddReceiveClientId(uid);
            }
            foreach (var sendArgs in redata.Values)
            {
                OnSend?.Invoke(this, sendArgs);
                var ServerKey = RedisKeyFormatUtil.GetServerKey(_appId, sendArgs.Server);
                _redis.Publish(ServerKey, JsonConvert.SerializeObject(notifications));
            }
        }
        #endregion

    }
}
