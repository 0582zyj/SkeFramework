using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Cache.Redis.Entities;

namespace SkeFramework.Cache.Redis.Helpers
{
   public class CommonUnitity
    {
       private static RedisConfig LocalConfig = null;
        /// <summary>
        /// 配置文件
        /// </summary>
       public static RedisConfig GlobalConfig
        {
            get
            {
                if (LocalConfig == null)
                {
                    LocalConfig = RedisConfig.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RedisConfig.xml"));
                }
                return LocalConfig;
            }
        }
    }
}
