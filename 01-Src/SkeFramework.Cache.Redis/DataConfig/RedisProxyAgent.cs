using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using SkeFramework.Cache.Redis.Entities;

namespace SkeFramework.Cache.Redis
{
    /// <summary>
    /// Redis配置管理器
    /// </summary>
    public class RedisProxyAgent
    {
        private static PooledRedisClientManager prcm;

        public static RedisConfig redisConfig { get; set; } = new RedisConfig();
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            string split = ",";
            string[] writeServerList = redisConfig.WriteServerConStr.Split(split.ToArray());
            string[] readServerList = redisConfig.ReadServerConStr.Split(split.ToArray());

            prcm = new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = redisConfig.MaxWritePoolSize,
                                 MaxReadPoolSize = redisConfig.MaxReadPoolSize,
                                 AutoStart = redisConfig.AutoStart,
                             });
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (prcm == null)
                CreateManager();
            return prcm.GetClient();
        }
    }
}
