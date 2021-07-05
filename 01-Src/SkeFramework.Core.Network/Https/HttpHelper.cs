using System;
using System.Net;
using Newtonsoft.Json;
using SkeFramework.Core.Network.Enums;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.Common.Enums;
using SkeFramework.Core.Network.Https.Services;
using System.Net.Http;

namespace SkeFramework.Core.Network.Https
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public class HttpHelper
    {
        private WebRequestUtil webUtils;

        public CookieContainer cookies = new CookieContainer();
        public HttpHelper()
        {
            webUtils = new WebRequestUtil();
        }

        #region 单例
        /// <summary>
        /// 静态实例
        /// </summary>
        private static HttpHelper example;
        public static HttpHelper Example
        {
            get
            {
                if (HttpHelper.example == null)
                {
                    HttpHelper.example = new HttpHelper();
                }
                return example;
            }
        }
        #endregion
        
        private void InitializeSessionToken(RequestBase request)
        {
            if (cookies.Count == 0)
                return;
            string Uri = request.Url;
            CookieCollection cookieCollection = cookies.GetCookies(new Uri(Uri));
            if (cookieCollection.Count == 0)
                return;
            request.SetValue("session_token", cookieCollection[0].Value,true);
        }

    /// <summary>
    /// 请求方法
    /// </summary>
    /// <param name="bPara">设置请求参数</param>
    /// <returns></returns>
    public string GetWebData(RequestBase request)
        {
            try
            {
                BrowserPara bPara = new BrowserPara();
                switch (request.contentType)
                {
                    case ContentTypeEnums.POSTDATA:
                        var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                        bPara.ContentType = ContentTypeEnums.POSTDATA.GetEnumDescription() + ";boundary = " + boundary;
                        break;
                    default:
                        bPara.ContentType = request.contentType.GetEnumDescription();
                        break;
                }
                InitializeSessionToken(request);
                bPara.Headers = request.HeaderValue;
                if (request.contentType== ContentTypeEnums.GETFORM)
                {
                    bPara.Uri = request.GetReqUrl();
                    bPara.Method = HttpMethod.Get.Method.ToString();
                    return webUtils.DoGet(bPara);
                }
                bPara.Uri = request.Url;
                bPara.PostData = request.GetRequestData();
                bPara.Method = HttpMethod.Post.Method.ToString();
                return webUtils.DoPost(bPara);
            }
            catch (Exception ex)
            {
                JsonResponses responses = new JsonResponses(JsonResponses.Failed.code, ex.Message);
                return JsonConvert.SerializeObject(responses);
            }
        }
    }
}