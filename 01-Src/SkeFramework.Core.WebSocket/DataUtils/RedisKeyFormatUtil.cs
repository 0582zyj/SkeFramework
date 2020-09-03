using SkeFramework.Core.WebSocketPush.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataUtils
{
    /// <summary>
    /// 
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
        /// 获取在线用户的RedisKey
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetOnLineClientKey(string appid)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_client_online}";      
        }
        /// <summary>
        /// 发布订阅事件Key
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public static string GetPublishChannelKey(string appid,string EventName)
        {
            return $"{RedisKey.ws_prefix}:{appid}:{RedisKey.ws_publish_channel}:{EventName}";

        }
    }
}
