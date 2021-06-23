using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkeFramework.Core.NetworkUtils.Bootstrap
{
    /// <summary>
    /// Session扩展类
    /// </summary>
    public static class SessionExtensions
    {
        #region 01-利用Newtonsoft.Json进行扩展
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        #endregion

    }
}
