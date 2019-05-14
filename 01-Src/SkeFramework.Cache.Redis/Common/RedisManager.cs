using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using SkeFramework.Cache.Redis.Helpers;

namespace SkeFramework.Cache.Redis
{
    /// <summary>
    /// Redis配置管理器
    /// </summary>
    public class RedisManager
    {
        private static PooledRedisClientManager prcm;
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            string split = ",";
            string[] writeServerList =CommonUnitity.GlobalConfig.WriteServerList.Split(split.ToArray());
            string[] readServerList = CommonUnitity.GlobalConfig.ReadServerList.Split(split.ToArray());

            prcm = new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = CommonUnitity.GlobalConfig.MaxWritePoolSize,
                                 MaxReadPoolSize = CommonUnitity.GlobalConfig.MaxReadPoolSize,
                                 AutoStart = CommonUnitity.GlobalConfig.AutoStart,
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
