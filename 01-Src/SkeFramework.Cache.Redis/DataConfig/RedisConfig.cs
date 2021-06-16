using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class RedisConfig : ConfigurationSection
    {
        #region 公开属性
        /// <summary>
        /// 可读的Redis链接地址【127.0.0.1:6379】
        /// </summary>
        [ConfigurationProperty("serverUrl", IsRequired = false)]
        public string serverUrl
        {
            get
            {
                return (string)base["serverUrl"];
            }
            set
            {
                base["serverUrl"] = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true)]
        public string password
        {
            get
            {
                return (string)base["password"];
            }
            set
            {
                base["password"] = value;
            }
        }
        /// <summary>
        /// Redis服务器数据库
        /// </summary>
        [ConfigurationProperty("defaultDatabase", IsRequired = false, DefaultValue = 0)]
        public int defaultDatabase
        {
            get
            {
                return (int)base["defaultDatabase"];
            }
            set
            {
                base["defaultDatabase"] = value;
            }
        }
        /// <summary>
        /// 连接池大小
        /// </summary>
        [ConfigurationProperty("poolsize", IsRequired = false, DefaultValue = 50)]
        public int poolsize
        {
            get
            {
                return (int)base["poolsize"];
            }
            set
            {
                base["poolsize"] = value;
            }
        }
        /// <summary>
        /// 初始链接大小
        /// </summary>
        [ConfigurationProperty("preheat", IsRequired = false, DefaultValue = 5)]
        public int preheat
        {
            get
            {
                return (int)base["preheat"];
            }
            set
            {
                base["preheat"] = value;
            }
        }
        /// <summary>
        /// 连接池中元素的空闲时间（MS），适用于连接远程redis服务器
        /// </summary>
        [ConfigurationProperty("idleTimeout", IsRequired = false, DefaultValue = 20000)]
        public int idleTimeout
        {
            get
            {
                int _idleTimeout = (int)base["idleTimeout"];
                return _idleTimeout > 0 ? _idleTimeout : 50;
            }
            set
            {
                base["idleTimeout"] = value;
            }
        }
        /// <summary>
        /// 连接超时 (MS)
        /// </summary>
        [ConfigurationProperty("connectTimeout", IsRequired = false, DefaultValue = 5000)]
        public int connectTimeout
        {
            get
            {
                int _connectTimeout = (int)base["connectTimeout"];
                return _connectTimeout > 0 ? _connectTimeout : 5000;
            }
            set
            {
                base["connectTimeout"] = value;
            }
        }

        /// <summary>
        /// 发送/接收超时 (MS)
        /// </summary>
        [ConfigurationProperty("syncTimeout", IsRequired = false, DefaultValue = 5000)]
        public int syncTimeout
        {
            get
            {
                int _syncTimeout = (int)base["syncTimeout"];
                return _syncTimeout > 0 ? _syncTimeout : 10000;
            }
            set
            {
                base["syncTimeout"] = value;
            }
        }
        #endregion

        #region The public method
        public static RedisConfig GetConfig()
        {
            RedisConfig section = GetConfig("CSRedisConfig");
            return section;
        }

        public static RedisConfig GetConfig(string sectionName)
        {
            RedisConfig section = (RedisConfig)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        /// <summary>
        /// 获取连接地址
        /// </summary>
        /// <returns></returns>
        public string GetRedisConnectionUrl()
        {
            List<string> server = new List<string>();
            server.Add(serverUrl);
            server.Add(defaultDatabase.ToString());
            server.Add(poolsize.ToString());
            server.Add(preheat.ToString());
            server.Add(idleTimeout.ToString());
            server.Add(connectTimeout.ToString());
            server.Add(syncTimeout.ToString());
            return String.Join( ",", server);
        }
        #endregion

    }
}
