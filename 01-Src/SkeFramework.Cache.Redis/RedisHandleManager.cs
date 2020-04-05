using SkeFramework.Cache.Redis.DataAccess;
using SkeFramework.Cache.Redis.DataHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Cache.Redis
{
   public class RedisHandleManager
    {
        
        private static RedisHandleManager _manager;
        public static RedisHandleManager Instance()
        {
            if (_manager == null)
            {
                _manager = new RedisHandleManager();
            }
            return _manager ?? (_manager = new RedisHandleManager());
        }


        private RedisString redisString;
        public RedisString RedisStringHandle
        {
            get {
                if (redisString == null)
                {
                    redisString = new RedisString();
                }
                return redisString;
            }
        }

        private RedisList redisList;

        public RedisList GetRedisListHandle()
        {
            if (redisList == null)
            {
                redisList = new RedisList();
            }
            return redisList;
        }

        private RedisStore redisStore;
        public RedisStore RedisStoreHandle
        {
            get
            {
                if (redisStore == null)
                {
                    redisStore = new RedisStore();
                }
                return redisStore;
            }
        }
        
    }
}
