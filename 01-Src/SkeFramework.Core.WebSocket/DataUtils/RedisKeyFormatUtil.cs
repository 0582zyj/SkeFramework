using SkeFramework.Core.WebSocketPush.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataUtils
{
    /// <summary>
    /// RedisKey格式化工具
    /// </summary>
    public class RedisKeyFormatUtil
    {
        /// <summary>
        /// 获取连接Token的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetConnectToken(string appid,string token)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_connect_token}:{token}";
        }

        /// <summary>
        /// 获取服务端的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetServerKey(string appid,string server)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_server}:{server}";
        }
        /// <summary>
        /// 获取在线用户的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetOnLineClientKey(string appid)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_client_online}";      
        }
        /// <summary>
        /// 发布事件的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public static string GetPublishChannelKey(string appid,string EventName)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_publish_channel}:{EventName}";
        }

        /// <summary>
        /// 订阅事件的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public static string GetSubscribeChannelKey(string appid, string ChannelName)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_subscribe_channel}:{ChannelName}";
        }
        /// <summary>
        /// 获取订阅列表的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetChannelListKey(string appid)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_channel_list}";
        }
        /// <summary>
        /// 获取用户订阅列表的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string GetClientChannelKey(string appid, Guid clientId)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_channel_client}:{clientId}";
        }
    }
}
