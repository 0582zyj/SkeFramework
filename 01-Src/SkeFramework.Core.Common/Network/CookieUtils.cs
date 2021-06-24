using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkeFramework.Core.Common.Network
{
    /// <summary>
    /// Cookie工具
    /// </summary>
    public class CookieUtils
    {
        /// <summary>         
        /// 解析Cookie         
        /// </summary>        
        private static readonly Regex RegexSplitCookie2 = new Regex(@"[^,][\S\s]+?;+[\S\s]+?(?=,\S)");
        /// <summary>         
        /// /// 获取所有Cookie 通过Set-Cookie         
        /// /// </summary>         
        /// /// <param name="setCookie"></param>         
        /// /// <returns></returns>         
        public static CookieCollection GetCookiesFromHeader(string setCookie)
        {
            var cookieCollection = new CookieCollection();
            //拆分Cookie           
            setCookie += ",T";//配合RegexSplitCookie2 加入后缀       
            var listStr = RegexSplitCookie2.Matches(setCookie);
            //循环遍历           
            foreach (Match item in listStr)
            {                 //根据; 拆分Cookie 内容     
                var cookieItem = item.Value.Split(';');
                var cookie = new Cookie();
                for (var index = 0; index < cookieItem.Length; index++)
                {
                    var info = cookieItem[index];
                    //第一个 默认 Cookie Name       
                    //判断键值对                  
                    if (info.Contains("="))
                    {
                        var indexK = info.IndexOf('=');
                        var name = info.Substring(0, indexK).Trim();
                        var val = info.Substring(indexK + 1);
                        if (index == 0)
                        {
                            cookie.Name = name;
                            cookie.Value = val;
                            continue;
                        }
                        if (name.Equals("Domain", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Domain = val;
                        }
                        else if (name.Equals("Expires", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime.TryParse(val, out var expires);
                            cookie.Expires = expires;
                        }
                        else if (name.Equals("Path", StringComparison.OrdinalIgnoreCase)) { cookie.Path = val; } else if (name.Equals("Version", StringComparison.OrdinalIgnoreCase)) { cookie.Version = Convert.ToInt32(val); }
                    }
                    else { if (info.Trim().Equals("HttpOnly", StringComparison.OrdinalIgnoreCase)) { cookie.HttpOnly = true; } }
                }
                cookieCollection.Add(cookie);
            }
            return cookieCollection;
        }
        /// <summary>      
        /// 获取 Cookies   
        /// </summary>        
        /// <param name="setCookie"></param>    
        /// <param name="uri"></param>      
        /// <returns></returns>     
        public static string GetCookies(string setCookie, Uri uri)
        {
            //获取所有Cookie 
            var strCookies = string.Empty;
            var cookies = GetCookiesFromHeader(setCookie);
            foreach (Cookie cookie in cookies)
            {
                //忽略过期Cookie      
                if (cookie.Expires < DateTime.Now && cookie.Expires != DateTime.MinValue)
                {
                    continue;
                }
                if (uri.Host.Contains(cookie.Domain))
                {
                    strCookies += $"{cookie.Name}={cookie.Value}; ";
                }
            }
            return strCookies;
        }

    }
}
