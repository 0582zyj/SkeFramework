using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkeFramework.Core.Common.Networks
{
    /// <summary>
    /// Cookie工具类
    /// </summary>
    public class CookieUtils
    {

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="sName">名称</param>
        /// <param name="sValue">值</param>
        public static void WriteCookie(string sName, string sValue)
        {
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            hca?.HttpContext?.Response.Cookies.Append(sName, sValue, option);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="sName">名称</param>
        /// <param name="sValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string sName, string sValue, int expires)
        {
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expires)
            };
            hca?.HttpContext?.Response.Cookies.Append(sName, sValue, option);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="sName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string sName)
        {
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            return hca?.HttpContext?.Request.Cookies[sName];
        }

        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="sName">Cookie对象名称</param>
        public static void RemoveCookie(string sName)
        {
            IHttpContextAccessor hca = GlobalContextUtils.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            hca?.HttpContext?.Response.Cookies.Delete(sName);
        }

    }
}
