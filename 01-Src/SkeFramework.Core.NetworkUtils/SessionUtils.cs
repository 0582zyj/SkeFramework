﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkeFramework.Core.Common.Networks;
using System;

namespace SkeFramework.Core.NetworkUtils
{
    public class SessionUtils
    {
        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public void WriteSession<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetService(Type.GetType("IHttpContextAccessor")) as IHttpContextAccessor;
            hca?.HttpContext?.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public void WriteSession(string key, string value)
        {
            WriteSession<string>(key, value);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public string GetSession(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetService(Type.GetType("IHttpContextAccessor")) as IHttpContextAccessor;
            return hca?.HttpContext?.Session.GetString(key) as string;
        }

        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public void RemoveSession(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetService(Type.GetType("IHttpContextAccessor")) as IHttpContextAccessor;
            hca?.HttpContext?.Session.Remove(key);
        }
    }
}
