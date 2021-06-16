using SkeFramework.Cache.Redis.DataAccess;
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
    }
}
