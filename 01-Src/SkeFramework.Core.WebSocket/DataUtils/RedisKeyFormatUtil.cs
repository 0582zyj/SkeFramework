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
        public static string GetServerKey(string appid, string server)
        {
            return $"{RedisKey.ws_prefix}:{RedisKey.ws_server}:{appid}:{server}";
        }

        /// <summary>
        /// 获取在线服务端的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetOnLineServerKey(string appid)
        {
            return $"{RedisKey.ws_prefix}:{RedisKey.ws_server_online}:{appid}";
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
            return $"{RedisKey.ws_prefix}:{RedisKey.ws_publish_channel}:{appid}:{EventName}";
        }

        /// <summary>
        /// 订阅事件的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public static string GetSubscribeChannelKey(string appid, string ChannelName)
        {
            return $"{RedisKey.ws_prefix}:{RedisKey.ws_subscribe_channel}:{appid}:{ChannelName}";
        }
        /// <summary>
        /// 获取订阅列表的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetChannelListKey(string appid)
        {
            return $"{RedisKey.ws_prefix}:{RedisKey.ws_channel_list}:{appid}";
        }
        /// <summary>
        /// 获取用户订阅列表的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string GetClientChannelKey(string appid, Guid clientId)
        {
            return $"{RedisKey.ws_prefix}:{RedisKey.ws_channel_client}:{appid}:{clientId}";
        }
    }
}
