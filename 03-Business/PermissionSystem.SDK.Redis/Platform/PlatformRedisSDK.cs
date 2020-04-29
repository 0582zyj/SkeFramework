using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;

using PermissionSystem.SDK.Redis.Constrant;
using ServiceStack.Text;
using SkeFramework.Cache.Redis;
using SkeFramework.Core.NetLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionSystem.SDK.Redis.Platform
{
    public class PlatformRedisSDK
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <param name="platforms"></param>
        /// <returns></returns>
        public bool SetPlatformList(long PlatformNo, List<OptionValue> platforms)
        {
            try
            {
                string key = String.Format("{0}_{1}", RedisKey.PlatformKeyPre, PlatformNo.ToString());
                string value = platforms.SerializeToString();
                RedisHandleManager.Instance().RedisStringHandle.Set(key, value);
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<OptionValue>  GetPlatformList(long PlatformNo)
        {
            try
            {
                string key = String.Format("{0}_{1}", RedisKey.PlatformKeyPre, PlatformNo.ToString());
                string value= RedisHandleManager.Instance().RedisStringHandle.Get(key);
                if (String.IsNullOrEmpty(value))
                {
                    return null;
                }
                value = JsonArrayObjects.Parse(value).ToJson();
                return value.FromJson<List<OptionValue>>();
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
            return null;
        }
    }
}
