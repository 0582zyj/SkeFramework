using CSRedis;
using SkeFramework.Core.WebSocketPush.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.API.RealTimeSystem.DataUtil
{
    public class RedisUtil
    {
        /// <summary>
        /// CSRedis 对象，用于存储数据和发送消息
        /// </summary>
        public static CSRedisClient Redis { get; set; }
        /// <summary>
        /// 根据用户获取推送ID的RedisKey
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static string GetUserIdRedisKey(string AppId, string UserId)
        {
            return $"{RedisKey.ws_prefix}:{AppId}:{RedisKey.ws_client_online}:{UserId}";
        }

        /// <summary>
        /// 根据用户获取推送ID的RedisKey
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static string GetWebSocketSessionID(string redisKey)
        {
            return RedisUtil.Redis.Get(redisKey);
        }
    }
}
