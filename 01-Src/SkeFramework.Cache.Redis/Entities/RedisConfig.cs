using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkeFramework.Cache.Redis.Entities
{
    /// <summary>
    /// Redis配置文件
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 写缓存地址【多个地址以逗号分开】
        /// </summary>
        private string writeServerList ;
        /// <summary>
        /// 读缓存地址【多个地址以逗号分开】
        /// </summary>
        private string readServerList = "127.0.0.1:6379";
        /// <summary>
        /// 最大写缓存池大小
        /// </summary>
        private int maxWritePoolSize = 60;
        /// <summary>
        /// 最小读缓存池大小
        /// </summary>
        private int maxReadPoolSize = 60;
        /// <summary>
        /// 本地缓存时间[S]
        /// </summary>
        private int localCacheTime = 1800;
        /// <summary>
        /// 自动开始
        /// </summary>
        private bool autoStart = true;

        #region 公开属性
        /// <summary>
        /// 写缓存地址【多个地址以逗号分开】
        /// </summary>
        public string WriteServerList
        {
            get { return String.IsNullOrEmpty(writeServerList)?"127.0.0.1:6379":writeServerList; }
            set { writeServerList = value; }
        }
        /// <summary>
        /// 读缓存地址【多个地址以逗号分开】
        /// </summary>
        public string ReadServerList
        {
            get { return String.IsNullOrEmpty(readServerList) ? "127.0.0.1:6379" : readServerList; }
            set { readServerList = value; }
        }
        /// <summary>
        /// 最大写缓存池大小
        /// </summary>
        public int MaxWritePoolSize
        {
            get { return maxWritePoolSize > 0 ? maxWritePoolSize : 5; }
            set { maxWritePoolSize = value; }
        }
        /// <summary>
        /// 最小读缓存池大小
        /// </summary>
        public int MaxReadPoolSize
        {
            get { return maxReadPoolSize > 0 ? maxReadPoolSize : 5; }
            set { maxReadPoolSize = value; }
        }
        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        public int LocalCacheTime
        {
            get { return localCacheTime > 0 ? localCacheTime : 36000; }
            set { localCacheTime = value; }
        }
        /// <summary>
        /// 自动开始
        /// </summary>
        public bool AutoStart
        {
            get { return autoStart; }
            set { autoStart = value; }
        }
        #endregion 

        #region The public method
        public static RedisConfig LoadConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(RedisConfig));
            StreamReader sr = new StreamReader(file);
            RedisConfig config = xs.Deserialize(sr) as RedisConfig;
            sr.Close();
            return config;
        }

        public void SaveConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(RedisConfig));
            StreamWriter sw = new StreamWriter(file);
            xs.Serialize(sw, this);
            sw.Close();
        }
        #endregion

    }
}
