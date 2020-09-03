using CSRedis;
using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities;
using SkeFramework.Core.WebSocketPush.DataEntities.Constants;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.DataUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushClients
{
    /// <summary>
    /// WebSocket客户端连接
    /// </summary>
    public class WebSocketSession
    {
        protected CSRedisClient _redis;
        protected List<string> _servers;
        protected string _redisPrefix;
        protected string _pathMatch;
        protected string appId;

        #region RedisKey
        /// <summary>
        /// 在线Redis的RedisKey
        /// </summary>
        protected string onlineKey { get { return RedisKeyFormatUtil.GetOnLineClientKey(appId); } }
        /// <summary>
        /// 上线事件的RedisKey
        /// </summary>
        protected string OnlineEventKey
        {
            get
            {
                return RedisKeyFormatUtil.GetPublishChannelKey(appId, RedisKey.ws_event_online);
            }
        }
        /// <summary>
        /// 下线事件的RedisKey
        /// </summary>
        protected string OfflineEventKey
        {
            get
            {
                return RedisKeyFormatUtil.GetPublishChannelKey(appId, RedisKey.ws_event_offline);
            }
        }
        #endregion

        /// <summary>
        /// 推送消息的事件，可审查推向哪个Server节点
        /// </summary>
        public EventHandler<NotificationsEventArgs> OnSend;

        /// <summary>
        /// 初始化 
        /// </summary>
        /// <param name="options"></param>
        public WebSocketSession(WebSocketSessionOptions options)
        {
            if (options.Redis == null) throw new ArgumentException("Redis 参数不能为空");
            if (options.Servers.Count == 0) throw new ArgumentException("Servers 参数不能为空");
            _redis = options.Redis;
            _servers = options.Servers;
            _redisPrefix = $"{RedisKey.ws_prefix}:{options.PathMatch.Replace('/', '_')}";
            appId = options.PathMatch.Trim('/').Replace('/', '_');
            _pathMatch = options.PathMatch ?? "/ws";
        }

        /// <summary>
        /// 负载分区规则：取clientId后四位字符，转成10进制数字0-65535，求模
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        protected string SelectServer(Guid clientId)
        {
            var servers_idx = int.Parse(clientId.ToString("N").Substring(28), NumberStyles.HexNumber) % _servers.Count;
            if (servers_idx >= _servers.Count) servers_idx = 0;
            return _servers[servers_idx];
        }

        /// <summary>
        /// 连接前的负载、授权，返回 ws 目标地址，使用该地址连接 websocket 服务端
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
                clientId = clientId,
                clientExtraProps = clientExtraProps
            };
            var tokenRedisKey = RedisKeyFormatUtil.GetConnectToken(this.appId, token);
            _redis.Set(tokenRedisKey, JsonConvert.SerializeObject(tokenValue), ConstData.TokenRxpireTime);
            return $"ws://{server}{_pathMatch}?token={token}";
        }


        /// <summary>
        /// 获取所在线客户端id
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Guid> GetClientListByOnline()
        {
            return _redis.HKeys(onlineKey).Select(a => Guid.TryParse(a, out var tryguid) ? tryguid : Guid.Empty).Where(a => a != Guid.Empty);
        }

        /// <summary>
        /// 判断客户端是否在线
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool HasOnline(Guid clientId)
        {
            return _redis.HGet<int>(onlineKey, clientId.ToString()) > 0;
        }

        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="receiveClientId">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        public void SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, object message, bool receipt = false)
        {
            receiveClientId = receiveClientId.Distinct().ToArray();
            Dictionary<string, NotificationsEventArgs> redata = new Dictionary<string, NotificationsEventArgs>();
            var messageJson = JsonConvert.SerializeObject(message);
            var Notifications = new WebSocketNotifications()
            {
                SenderClientId = senderClientId,
                ReceiveClientId = receiveClientId.ToList(),
                Message = messageJson,
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
                _redis.Publish($"{_redisPrefix}Server{sendArgs.Server}",
                    JsonConvert.SerializeObject(Notifications));
            }
        }


        /// <summary>
        /// 事件订阅
        /// </summary>
        /// <param name="online">上线</param>
        /// <param name="offline">下线</param>
        public void EventBus(
            Action<(Guid clientId, string clientMetaData)> online,
            Action<(Guid clientId, string clientMetaData)> offline)
        {

            _redis.Subscribe(
                (OnlineEventKey, msg => online(JsonConvert.DeserializeObject<(Guid clientId, string clientMetaData)>(msg.Body))),
                (OfflineEventKey, msg => offline(JsonConvert.DeserializeObject<(Guid clientId, string clientMetaData)>(msg.Body))));
        }

        #region 群聊频道，每次上线都必须重新加入

        /// <summary>
        /// 加入群聊频道，每次上线都必须重新加入
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chan">群聊频道名</param>
        public void JoinChan(Guid clientId, string chan)
        {
            _redis.StartPipe(a => a
                .HSet($"{_redisPrefix}Chan{chan}", clientId.ToString(), 0)
                .HSet($"{_redisPrefix}Client{clientId}", chan, 0)
                .HIncrBy($"{_redisPrefix}ListChan", chan, 1));
        }
        /// <summary>
        /// 离开群聊频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chans">群聊频道名</param>
        public void LeaveChan(Guid clientId, params string[] chans)
        {
            if (chans?.Any() != true) return;
            using (var pipe = _redis.StartPipe())
            {
                foreach (var chan in chans)
                    pipe
                        .HDel($"{_redisPrefix}Chan{chan}", clientId.ToString())
                        .HDel($"{_redisPrefix}Client{clientId}", chan)
                        .Eval($"if redis.call('HINCRBY', KEYS[1], '{chan}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{chan}') end return 1",
                            $"{_redisPrefix}ListChan");
            }
        }
        /// <summary>
        /// 获取群聊频道所有客户端id（测试）
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        /// <returns></returns>
        public Guid[] GetChanClientList(string chan)
        {
            return _redis.HKeys($"{_redisPrefix}Chan{chan}").Select(a => Guid.Parse(a)).ToArray();
        }
        /// <summary>
        /// 清理群聊频道的离线客户端（测试）
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        public void ClearChanClient(string chan)
        {
            var websocketIds = _redis.HKeys($"{_redisPrefix}Chan{chan}");
            var offline = new List<string>();
            var span = websocketIds.AsSpan();
            var start = span.Length;
            while (start > 0)
            {
                start = start - 10;
                var length = 10;
                if (start < 0)
                {
                    length = start + 10;
                    start = 0;
                }
                var slice = span.Slice(start, length);
                string onlineKey = RedisKeyFormatUtil.GetOnLineClientKey(appId);
                var hvals = _redis.HMGet(onlineKey, slice.ToArray().Select(b => b.ToString()).ToArray());
                for (var a = length - 1; a >= 0; a--)
                {
                    if (string.IsNullOrEmpty(hvals[a]))
                    {
                        offline.Add(span[start + a]);
                        span[start + a] = null;
                    }
                }
            }
            //删除离线订阅
            if (offline.Any()) _redis.HDel($"{_redisPrefix}Chan{chan}", offline.ToArray());
        }

        /// <summary>
        /// 获取所有群聊频道和在线人数
        /// </summary>
        /// <returns>频道名和在线人数</returns>
        public IEnumerable<(string chan, long online)> GetChanList()
        {
            var ret = _redis.HGetAll<long>($"{_redisPrefix}ListChan");
            return ret.Select(a => (a.Key, a.Value));
        }
        /// <summary>
        /// 获取用户参与的所有群聊频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        public string[] GetChanListByClientId(Guid clientId)
        {
            return _redis.HKeys($"{_redisPrefix}Client{clientId}");
        }
        /// <summary>
        /// 获取群聊频道的在线人数
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        /// <returns>在线人数</returns>
        public long GetChanOnline(string chan)
        {
            return _redis.HGet<long>($"{_redisPrefix}ListChan", chan);
        }

        /// <summary>
        /// 发送群聊消息，所有在线的用户将收到消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="chan">群聊频道名</param>
        /// <param name="message">消息</param>
        public void SendChanMessage(Guid senderClientId, string chan, object message)
        {
            var websocketIds = _redis.HKeys($"{_redisPrefix}Chan{chan}");
            IEnumerable<Guid> receiveClientId = websocketIds.Where(a => !string.IsNullOrEmpty(a))
                .Select(a => Guid.TryParse(a, out var tryuuid) ? tryuuid : Guid.Empty).ToArray();
            SendMessage(Guid.Empty, receiveClientId, message);
        }
        #endregion
    }
}
