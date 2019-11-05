using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Network.Enums;

namespace SkeFramework.Core.Network.Https
{
    /// <summary>
    ///请求参数类
    /// </summary>
    public class BrowserPara
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public RequestTypeEnums Method = RequestTypeEnums.POST;// public string Method = "Post";
        /// <summary>
        /// 请求参数
        /// </summary>
        public string PostData = string.Empty;

        /// <summary>
        /// 请求RefererHttp头
        /// </summary>              
        public string Referer = string.Empty;

        /// <summary>
        /// Cookies集合
        /// </summary>
        public Dictionary<string, string> Cookies;

        /// <summary>
        /// 请求协议头 Key/Value
        /// </summary>
        public Dictionary<string, string> Headers;
        /// <summary>
        /// 请求的媒体型
        /// </summary>
        public string MediaType = "text/html; charset=UTF-8";

        /// <summary>
        /// 请求Http Accept值 
        /// </summary>
        public string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;";

        /// <summary>
        /// 请求的User-Agent值 
        /// </summary>
        public string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";

        /// <summary>
        /// 请求的content-Type值 
        /// </summary>
        public string contentTypeGet = "application/x-www-form-urlencoded";
        //public string contentType = "multipart/form-data";

    }
}
