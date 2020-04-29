using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using SkeFramework.Cache.Redis.DataAccess.Bases;

namespace SkeFramework.Cache.Redis.DataAccess
{
    /// <summary>
    /// Redis数据类型List
    /// </summary>
    public class RedisList:RedisBase
    {
        #region 赋值
        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public void LPush(string key,string value)
        {
            RedisBase.redisClient.PushItemToList(key,value);
        }
        /// <summary>
        /// 从左侧向list中添加值，并设置过期时间
        /// </summary>
        public void LPush(string key, string value,DateTime dt)
        {
            RedisBase.redisClient.PushItemToList(key, value);
            RedisBase.redisClient.ExpireEntryAt(key,dt);
        }
        /// <summary>
        /// 从左侧向list中添加值，设置过期时间
        /// </summary>
        public void LPush(string key, string value, TimeSpan sp)
        {
            RedisBase.redisClient.PushItemToList(key, value);
            RedisBase.redisClient.ExpireEntryIn(key, sp);
        }
        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public void RPush(string key, string value)
        {
            RedisBase.redisClient.PrependItemToList(key,value);
        }
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>    
        public void RPush(string key, string value, DateTime dt)
        {
            RedisBase.redisClient.PrependItemToList(key, value);
            RedisBase.redisClient.ExpireEntryAt(key, dt);
        }
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>        
        public void RPush(string key, string value, TimeSpan sp)
        {
            RedisBase.redisClient.PrependItemToList(key, value);
            RedisBase.redisClient.ExpireEntryIn(key, sp);
        }
        /// <summary>
        /// 添加key/value
        /// </summary>     
        public void Add(string key, string value)
        {
            RedisBase.redisClient.AddItemToList(key,value);
        }
        /// <summary>
        /// 添加key/value ,并设置过期时间
        /// </summary>  
        public void Add(string key, string value,DateTime dt)
        {
            RedisBase.redisClient.AddItemToList(key, value);
            RedisBase.redisClient.ExpireEntryAt(key,dt);
        }
        /// <summary>
        /// 添加key/value。并添加过期时间
        /// </summary>  
        public void Add(string key, string value,TimeSpan sp)
        {
            RedisBase.redisClient.AddItemToList(key, value);
            RedisBase.redisClient.ExpireEntryIn(key,sp);
        }
        /// <summary>
        /// 为key添加多个值
        /// </summary>  
        public void Add(string key, List<string> values)
        {
            RedisBase.redisClient.AddRangeToList(key,values);
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public void Add(string key, List<string> values,DateTime dt)
        {
            RedisBase.redisClient.AddRangeToList(key, values);
            RedisBase.redisClient.ExpireEntryAt(key,dt);
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public void Add(string key, List<string> values,TimeSpan sp)
        {
            RedisBase.redisClient.AddRangeToList(key, values);
            RedisBase.redisClient.ExpireEntryIn(key,sp);
        }
        #endregion
        #region 获取值
        /// <summary>
        /// 获取list中key包含的数据数量
        /// </summary>  
        public long Count(string key)
        {
            return RedisBase.redisClient.GetListCount(key);
        }
        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        public List<string> Get(string key)
        {
            return RedisBase.redisClient.GetAllItemsFromList(key);
        }
        /// <summary>
        /// 获取key中下标为star到end的值集合
        /// </summary>  
        public List<string> Get(string key,int star,int end)
        {
            return RedisBase.redisClient.GetRangeFromList(key,star,end);
        }        
        #endregion
        #region 阻塞命令
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingPopItemFromList(string key,TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingDequeueItemFromList(key,sp);
        }
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public ItemRef BlockingPopItemFromLists(string[] keys, TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingPopItemFromLists(keys, sp);
        }
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingDequeueItemFromList(string key, TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingDequeueItemFromList(key, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public ItemRef BlockingDequeueItemFromLists(string[] keys, TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingDequeueItemFromLists(keys, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中key的头部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingRemoveStartFromList(string keys, TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingRemoveStartFromList(keys, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中key的头部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public ItemRef BlockingRemoveStartFromLists(string[] keys, TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingRemoveStartFromLists(keys, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中一个fromkey的尾部移除一个值，添加到另外一个tokey的头部，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingPopAndPushItemBetweenLists(string fromkey, string tokey, TimeSpan? sp)
        {
            return RedisBase.redisClient.BlockingPopAndPushItemBetweenLists(fromkey, tokey, sp);
        }
        #endregion
        #region 删除
        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        public string PopItemFromList(string key)
        {
            return RedisBase.redisClient.PopItemFromList(key);
        }
        /// <summary>
        /// 移除list中，key/value,与参数相同的值，并返回移除的数量
        /// </summary>  
        public long RemoveItemFromList(string key,string value)
        {
            return RedisBase.redisClient.RemoveItemFromList(key,value);
        }
        /// <summary>
        /// 从list的尾部移除一个数据，返回移除的数据
        /// </summary>  
        public string RemoveEndFromList(string key)
        {
            return RedisBase.redisClient.RemoveEndFromList(key);
        }
        /// <summary>
        /// 从list的头部移除一个数据，返回移除的值
        /// </summary>  
        public string RemoveStartFromList(string key)
        {
            return RedisBase.redisClient.RemoveStartFromList(key);
        }       
        #endregion
        #region 其它
        /// <summary>
        /// 从一个list的尾部移除一个数据，添加到另外一个list的头部，并返回移动的值
        /// </summary>  
        public string PopAndPushItemBetweenLists(string fromKey, string toKey)
        {
            return RedisBase.redisClient.PopAndPushItemBetweenLists(fromKey,toKey);
        }
        #endregion



    }
}
