using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using SkeFramework.Cache.Redis.Entities;

namespace SkeFramework.Cache.Redis
{
    /// <summary>
    /// Redis配置管理器
    /// </summary>
    public class RedisProxyAgent
    {

        public static RedisConfig redisConfig { get; set; } = new RedisConfig();
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            RedisHelper.Initialization(new CSRedisClient(redisConfig.GetRedisConnectionUrl()));
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static CSRedisClient GetClient()
        {
            if (RedisHelper.Instance == null)
            {
                CreateManager();
            }
            return RedisHelper.Instance;
        }
    }
}
