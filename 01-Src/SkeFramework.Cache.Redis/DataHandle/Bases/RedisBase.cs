using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRedis;

namespace SkeFramework.Cache.Redis.DataAccess.Bases
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        /// <summary>
        /// redis客户端
        /// </summary>
        public static CSRedisClient redisClient { get; private set; }
        private bool _disposed = false;
        static RedisBase()
        {
            redisClient = RedisProxyAgent.GetClient();
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    redisClient.Dispose();
                    redisClient = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
          
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            
        }
    }
}
