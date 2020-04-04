using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Cache.Redis.DataAccess.Bases;

namespace SkeFramework.Cache.Redis.DataAccess
{
    /// <summary>
    /// Redis数据类型哈希Hash
    /// </summary>
    public class RedisHash :RedisBase
    {
        #region 添加
        /// <summary>
        /// 向hashid集合中添加key/value
        /// </summary>       
        public bool SetEntryInHash(string hashid, string key, string value)
        {
            return RedisBase.redisClient.SetEntryInHash(hashid,key,value);
        }
        /// <summary>
        /// 如果hashid集合中存在key/value则不添加返回false，如果不存在在添加key/value,返回true
        /// </summary>
        public bool SetEntryInHashIfNotExists(string hashid, string key, string value)
        {
            return RedisBase.redisClient.SetEntryInHashIfNotExists(hashid, key, value);
        }
        /// <summary>
        /// 存储对象T t到hash集合中
        /// </summary>
        public void StoreAsHash<T>(T t)
        {
            RedisBase.redisClient.StoreAsHash<T>(t);
        }
        #endregion
        #region 获取
        /// <summary>
        /// 获取对象T中ID为id的数据。
        /// </summary>
        public T GetFromHash<T>(object id)
        {
            return RedisBase.redisClient.GetFromHash<T>(id);
        }
        /// <summary>
        /// 获取所有hashid数据集的key/value数据集合
        /// </summary>
        public Dictionary<string, string> GetAllEntriesFromHash(string hashid)
        {
            return RedisBase.redisClient.GetAllEntriesFromHash(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中的数据总数
        /// </summary>
        public long GetHashCount(string hashid)
        {
            return RedisBase.redisClient.GetHashCount(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中所有key的集合
        /// </summary>
        public List<string> GetHashKeys(string hashid)
        {
            return RedisBase.redisClient.GetHashKeys(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中的所有value集合
        /// </summary>
        public List<string> GetHashValues(string hashid)
        {
            return RedisBase.redisClient.GetHashValues(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中，key的value数据
        /// </summary>
        public string GetValueFromHash(string hashid, string key)
        {
            return RedisBase.redisClient.GetValueFromHash(hashid, key);
        }
        /// <summary>
        /// 获取hashid数据集中，多个keys的value集合
        /// </summary>
        public List<string> GetValuesFromHash(string hashid, string[] keys)
        {
            return RedisBase.redisClient.GetValuesFromHash(hashid, keys);
        }
        #endregion
        #region 删除
        #endregion
        /// <summary>
        /// 删除hashid数据集中的key数据
        /// </summary>
        public bool RemoveEntryFromHash(string hashid, string key)
        {
            return RedisBase.redisClient.RemoveEntryFromHash(hashid, key);
        }     
        #region 其它
        /// <summary>
        /// 判断hashid数据集中是否存在key的数据
        /// </summary>
        public bool HashContainsEntry(string hashid, string key)
        {
            return RedisBase.redisClient.HashContainsEntry(hashid,key);
        }
        /// <summary>
        /// 给hashid数据集key的value加countby，返回相加后的数据
        /// </summary>
        public double IncrementValueInHash(string hashid, string key, int countBy)
        {
            return RedisBase.redisClient.IncrementValueInHash(hashid, key, countBy);
        }
        #endregion
    }
}
