using CSRedis;
using SkeFramework.Core.WebSocketPush.DataEntities;
using SkeFramework.Core.WebSocketPush.DataUtils;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushBrokers
{
    /// <summary>
    /// 推送核心配置
    /// </summary>
    public class WebSocketBorker
    {
        /// <summary>
        /// Redis客户端
        /// </summary>
        protected CSRedisClient _redis;
        /// <summary>
        /// 集群服务端列表
        /// </summary>
        protected List<string> _servers;
        /// <summary>
        /// WS-Path
        /// </summary>
        protected string _pathMatch;
        /// <summary>
        /// 应用ID 
        /// </summary>
        protected string _appId;

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
            _appId = configs.PathMatch.Trim('/').Replace('/', '_');
            _pathMatch = configs.PathMatch ?? "/ws";
        }
    }
}
