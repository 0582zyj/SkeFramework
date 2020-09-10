using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.DataUtils;
using SkeFramework.Core.WebSocketPush.PushServices.PushBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushClients
{
    /// <summary>
    /// 订阅服务
    /// </summary>
    public class WebSocketChannelClient : WebSocketBorker, IPushBroker
    {
        /// <summary>
        /// 基础通信类
        /// </summary>
        protected WebSocketClient SocketClient = null;
        /// <summary>
        ///构造函数 
        /// </summary>
        /// <param name="configs"></param>
        public WebSocketChannelClient(WebSocketClientConfig configs)
            : base(configs)
        {
            SocketClient = new WebSocketClient(configs);
        }

        #region 订阅管理
        /// <summary>
        /// 加入订阅
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="channel">订阅名称</param>
        public void SubscribeChannel(Guid clientId, string channel)
        {
            string channelKey = RedisKeyFormatUtil.GetSubscribeChannelKey(_appId, channel);
            string ClientChannelKey = RedisKeyFormatUtil.GetClientChannelKey(_appId, clientId);
            _redis.StartPipe(a => a
                .HSet(channelKey, clientId.ToString(), 0)
                .HSet(ClientChannelKey, channel, 0)
                .HIncrBy(this.ChannelListKey, channel, 1));
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="channels">订阅名称</param>
        public void UnSubscribeChannel(Guid clientId, params string[] channels)
        {
            if (channels?.Any() != true) return;
            using (var pipe = _redis.StartPipe())
            {
                string ClientChannelKey = RedisKeyFormatUtil.GetClientChannelKey(_appId, clientId);
                foreach (var channel in channels)
                {
                    string channelKey = RedisKeyFormatUtil.GetSubscribeChannelKey(_appId, channel);
                    pipe.HDel(channelKey, clientId.ToString())
                        .HDel(ClientChannelKey, channel)
                        .Eval($"if redis.call('HINCRBY', KEYS[1], '{channel}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{channel}') end return 1",
                            this.ChannelListKey);
                }

            }
        }
        /// <summary>
        /// 清楚订阅的离线客户端
        /// </summary>
        /// <param name="channel">订阅名称</param>
        public void ClearChannelClient(string channel)
        {
            string channelKey = RedisKeyFormatUtil.GetSubscribeChannelKey(_appId, channel);
            var websocketIds = _redis.HKeys(channelKey);
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
                string onlineKey = RedisKeyFormatUtil.GetOnLineClientKey(_appId);
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
            if (offline.Any())
            {
                _redis.HDel(channelKey, offline.ToArray());
            }
        }
        #endregion
        #region 获取订阅信息
        /// <summary>
        /// 获取订阅的客户端列表
        /// </summary>
        /// <param name="channel">订阅名称</param>
        /// <returns></returns>
        public Guid[] GetChannelClientList(string channel)
        {
            string channelKey = RedisKeyFormatUtil.GetSubscribeChannelKey(_appId, channel);
            return _redis.HKeys(channelKey).Select(a => Guid.Parse(a)).ToArray();
        }
        /// <summary>
        /// 获取用户参与的所有订阅
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        public string[] GetChanListByClientId(Guid clientId)
        {
            string ClientChannelKey = RedisKeyFormatUtil.GetClientChannelKey(_appId, clientId);
            return _redis.HKeys(ClientChannelKey);
        }
        /// <summary>
        /// 获取订阅的在线人数
        /// </summary>
        /// <param name="chan">订阅名称</param>
        /// <returns>在线人数</returns>
        public long GetChanOnline(string chan)
        {
            return _redis.HGet<long>(this.ChannelListKey, chan);
        }
        /// <summary>
        /// 获取所有订阅和在线人数
        /// </summary>
        /// <returns>名和在线人数</returns>
        public IEnumerable<OnlineChannelVo> GetChannelList()
        {
            var ret = _redis.HGetAll<long>(this.ChannelListKey);
            return ret.Select(a => new OnlineChannelVo(){
                Channel=a.Key,
                Online=a.Value
            });
        }
        #endregion
        #region 发送信息

        /// <summary>
        /// 发送订阅消息，所有在线的用户将收到消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="channel">订阅名称</param>
        /// <param name="message">消息</param>
        public void SendChanMessage(Guid senderClientId, string channel, string message)
        {
            string channelKey = RedisKeyFormatUtil.GetSubscribeChannelKey(_appId, channel);
            var websocketIds = _redis.HKeys(channelKey);
            IEnumerable<Guid> receiveClientId = websocketIds.Where(a => !string.IsNullOrEmpty(a))
                .Select(a => Guid.TryParse(a, out var tryuuid) ? tryuuid : Guid.Empty).ToArray();
            this.SendMessage(Guid.Empty, receiveClientId, message);
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
            ((IPushBroker)SocketClient).SendMessage(senderClientId, receiveClientId, message, receipt);
        }
        #endregion
    }
}
