using SkeFramework.Core.NetLog;
using SkeFramework.Core.Network.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.Https.Services
{
    /// <summary>
    /// 作用描述： HttpWebRequest操作类
    /// </summary>
    public class HttpWebRequestUtil
    {

        public static string HttpGet(BrowserPara browserPara)
        {
            try
            {
                LogAgent.Info("[Request]->Url:{0};Parameter:{1};Method:{2}", browserPara.Uri, browserPara.PostData, browserPara.Method.ToString());
                HttpWebRequest request = WebRequest.Create(browserPara.Uri) as HttpWebRequest;
                //每次请求绕过代理，解决第一次调用慢的问题
                request.Proxy = null;
                //多线程并发调用时默认2个http连接数限制的问题，讲其设为1000
                ServicePoint currentServicePoint = request.ServicePoint;
                currentServicePoint.ConnectionLimit = 1000;
                if (browserPara.Headers != null)
                {
                    foreach (var header in browserPara.Headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                string str;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        str = reader.ReadToEnd();
                    }
                    request.Abort();
                    request = null;
                }
                LogAgent.Info("[Response]->Url:{0};{1}", browserPara.Uri, str);
                return str;
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
            return String.Empty;
        }

        public static string HttpPost(BrowserPara browserPara)
        {
            try
            {
                LogAgent.Info("[Request]->Url:{0};Parameter:{1};Method:{2}", browserPara.Uri, browserPara.PostData, browserPara.Method.ToString());
                HttpWebRequest request = WebRequest.Create(browserPara.Uri) as HttpWebRequest;
                //将发送数据转换为二进制格式
                byte[] byteArray = Encoding.UTF8.GetBytes(browserPara.PostData);
                //要POST的数据大于1024字节的时候, 浏览器并不会直接就发起POST请求, 而是会分为俩步：
                //1. 发送一个请求, 包含一个Expect:100-continue, 询问Server使用愿意接受数据
                //2. 接收到Server返回的100-continue应答以后, 才把数据POST给Server
                //直接关闭第一步验证
                request.ServicePoint.Expect100Continue = false;
                //是否使用Nagle：不使用，提高效率 
                request.ServicePoint.UseNagleAlgorithm = false;
                //设置最大连接数
                request.ServicePoint.ConnectionLimit = 65500;
                //指定压缩方法
                //request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.KeepAlive = false;
                request.ContentType = browserPara.contentTypeGet;
                request.Method = HttpMethod.Post.Method.ToString(); 
                request.Timeout = browserPara.Timeout;
                request.ContentLength = byteArray.Length;
                //关闭缓存
                request.AllowWriteStreamBuffering = false;
                //每次请求绕过代理，解决第一次调用慢的问题
                request.Proxy = null;
                //多线程并发调用时默认2个http连接数限制的问题，讲其设为1000
                ServicePoint currentServicePoint = request.ServicePoint;
                if (browserPara.Headers != null)
                {
                    foreach (var header in browserPara.Headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                string responseFromServer = String.Empty;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    WebResponse response = request.GetResponse();
                    //获取服务器返回的数据流
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                    request.Abort();
                }
                LogAgent.Info("[Response]->Url:{0};{1}", browserPara.Uri, responseFromServer);
                return responseFromServer;
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
            return String.Empty;
        }

    }
}
