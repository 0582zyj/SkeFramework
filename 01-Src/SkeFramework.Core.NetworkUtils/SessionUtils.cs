﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SkeFramework.Core.Common.Networks;
using System;

namespace SkeFramework.Core.NetworkUtils
{
    /// <summary>
    /// Session工具
    /// </summary>
    public class SessionUtils
    {
        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                return;
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            hca?.HttpContext?.Session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>   
        public static T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default(T);
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            string value= hca?.HttpContext?.Session.GetString(key) as string;
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// 获取SessionId
        /// </summary>
        /// <param name="session_token"></param>
        /// <returns></returns>
        public static String GetSessionId(string session_token)
        {
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            RequestHeaders requestHeaders = hca?.HttpContext?.Request.GetTypedHeaders();
            string sessionToken = requestHeaders?.Get<string>(session_token);
            if (!String.IsNullOrEmpty(sessionToken))
                return sessionToken;
            return hca?.HttpContext?.Session.Id;
        }
    }
}
