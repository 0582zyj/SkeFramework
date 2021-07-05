using SkeFramework.Core.NetLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SkeFramework.Core.Network.Https.Services
{
    /// <summary>
    /// 网络工具类。
    /// </summary>
    public sealed class WebRequestUtil
    {
        private bool _ignoreSSLCheck = true;
       
        /// <summary>
        /// 是否忽略SSL检查
        /// </summary>
        public bool IgnoreSSLCheck
        {
            get { return this._ignoreSSLCheck; }
            set { this._ignoreSSLCheck = value; }
        }
    
        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(BrowserPara browserPara)
        {
            LogAgent.Info("[Request]->Url:{0};Parameter:{1};Method:{2}", browserPara.Uri, browserPara.PostData, browserPara.Method.ToString());
            string url = browserPara.Uri;
            HttpWebRequest req = GetWebRequest(browserPara,"Post");
            req.ContentType = browserPara.ContentType;
            byte[] postData = Encoding.UTF8.GetBytes(browserPara.PostData);
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = GetResponseEncoding(rsp);
            string responseFromServer = GetResponseAsString(req, rsp, encoding);
            
            LogAgent.Info("[Response]->Url:{0};{1}", browserPara.Uri, responseFromServer);
            return responseFromServer;
        }
        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(BrowserPara browserPara )
        {
            LogAgent.Info("[Request]->Url:{0};Parameter:{1};Method:{2}", browserPara.Uri, browserPara.PostData, browserPara.Method.ToString());
            HttpWebRequest req = GetWebRequest(browserPara, "GET");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = GetResponseEncoding(rsp);
            string responseFromServer = GetResponseAsString(req, rsp, encoding);
            LogAgent.Info("[Response]->Url:{0};{1}", browserPara.Uri, responseFromServer);
            return responseFromServer;
        }
        /// <summary>
        /// 执行带body体的POST请求。
        /// </summary>
        /// <param name="url">请求地址，含URL参数</param>
        /// <param name="body">请求body体字节流</param>
        /// <param name="contentType">body内容类型</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPostJson(BrowserPara browserPara )
        {
            LogAgent.Info("[Request]->Url:{0};Parameter:{1};Method:{2}", browserPara.Uri, browserPara.PostData, browserPara.Method.ToString());
            string url = browserPara.Uri;
            HttpWebRequest req = GetWebRequest(browserPara, "POST");
            req.ContentType = browserPara.ContentType;
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线 
            req.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            System.IO.Stream reqStream = req.GetRequestStream();
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            reqStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = GetResponseEncoding(rsp);
            string responseFromServer= GetResponseAsString(req, rsp, encoding);
            LogAgent.Info(req.CookieContainer.GetCookieHeader(new Uri(browserPara.Uri)));
            LogAgent.Info("[Response]->Url:{0};{1}", browserPara.Uri, responseFromServer);
            return responseFromServer;
        }

        private HttpWebRequest GetWebRequest(BrowserPara browserPara,string method)
        {
            string url = browserPara.Uri;
            IDictionary< string, string> headerParams = browserPara.Headers;
            HttpWebRequest req = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                if (this._ignoreSSLCheck)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(TrustAllValidationCallback);
                }
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(url);
            }


            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (string key in headerParams.Keys)
                {
                    req.Headers.Add(key, headerParams[key]);
                }
            }
          
            req.CookieContainer = HttpHelper.Example.cookies;
          
            req.Proxy = null;
            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.KeepAlive = true;
            req.UserAgent = browserPara.UserAgent;
            req.Accept = browserPara.Accept;
            req.Timeout = browserPara.Timeout;
            req.ReadWriteTimeout = browserPara.ReadWriteTimeout;
            return req;
        }
        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        private string GetResponseAsString(HttpWebRequest req,HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {                
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                if (HttpHelper.Example.cookies.Count == 0)
                {
                    HttpHelper.Example.cookies = req.CookieContainer;
                }
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
        private Encoding GetResponseEncoding(HttpWebResponse rsp)
        {
            string charset = rsp.CharacterSet;
            if (string.IsNullOrEmpty(charset))
            {
                charset = Constants.CHARSET_UTF8;
            }
            return Encoding.GetEncoding(charset);
        }
        private static bool TrustAllValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; // 忽略SSL证书检查
        }
    }

    public class Constants
    {
        public const string CHARSET_UTF8 = "utf-8";
    }

}