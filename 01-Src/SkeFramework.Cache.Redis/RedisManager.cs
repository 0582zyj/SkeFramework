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
    public class RedisManager
    {
        private static PooledRedisClientManager prcm;
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfig RedisConfig = new RedisConfig();

        public static RedisConfig redisConfig { get { return RedisConfig; } set { RedisConfig = value; } }
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            string split = ",";
            string[] writeServerList = RedisConfig.WriteServerConStr.Split(split.ToArray());
            string[] readServerList = RedisConfig.ReadServerConStr.Split(split.ToArray());

            prcm = new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = RedisConfig.MaxWritePoolSize,
                                 MaxReadPoolSize = RedisConfig.MaxReadPoolSize,
                                 AutoStart = RedisConfig.AutoStart,
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
