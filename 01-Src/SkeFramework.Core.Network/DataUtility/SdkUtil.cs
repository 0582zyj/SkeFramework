using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SkeFramework.Core.Common.Collections;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Network.Https;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.DataUtility
{
    /// <summary>
    /// Ske平台请求工具类
    /// </summary>
    public class SdkUtil
    {
        /// <summary>
        /// 请求获取实体【失败则返回实体默认值】
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public List<T> PostForResultListVo<T>(RequestBase request)
        {
            JsonResponses resultObject = PostForVo(request);
            if (resultObject.ValidateResponses() && resultObject != null)
            {
                return resultObject.GetDataList<T>();
            }
            return null;
        }
        /// <summary>
        /// 请求获取实体【失败则返回实体默认值】
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public T PostForResultVo<T>(RequestBase request)
        {
            JsonResponses resultObject = PostForVo(request);
            if (resultObject.ValidateResponses() && resultObject != null)
            {
                return resultObject.GetDataValue<T>();
            }
            return default(T);
        }
        /// <summary>
        /// 请求返回统一消息结构
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public JsonResponses PostForVo(RequestBase request)
        {
            string result = post(request);
            if (String.IsNullOrEmpty(result))
                return JsonResponses.Failed;
            var format = "yyyy-MM-dd hh:mm:ss.fff"; // your datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
            return JsonConvert.DeserializeObject<JsonResponses>(result, dateTimeConverter);
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        private string post(RequestBase request)
        {
            ////initLanguageHeader(headers);
            InitializeSessionToken(request);
            ////initAppIdHeader(headers, t);
            return HttpHelper.Example.GetWebData(request);
        }
        private void InitializeSessionToken(RequestBase request)
        {
            if (!String.IsNullOrEmpty(HttpHelper.Example.SessionId))
            {
                request.SetValue("session_token", HttpHelper.Example.SessionId, true);
                return;
            }
            if (HttpHelper.Example.cookies.Count == 0)
                return;
            string Uri = request.Url;
            CookieCollection cookieCollection = HttpHelper.Example.cookies.GetCookies(new Uri(Uri));
            if (cookieCollection.Count == 0)
                return;
            request.SetValue("session_token", cookieCollection[0].Value, true);
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="R"></typeparam>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public JsonResponses postModelForVo<R, T>(string url, R obj)
        //{
        //    JsonResponses resultObject = postModel(url, obj);
        //    JsonResponses resultVo = resultObject.Clone();
        //    if (resultObject.ValidateResponses() && resultObject != null)
        //    {
        //        resultVo.data = resultObject.GetDataValue<T>();
        //    }
        //    return resultVo;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public JsonResponses postModel<T>(string url, T obj)
        //{
        //    string result = post(url, obj);
        //    return JsonConvert.DeserializeObject<JsonResponses>(result);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="t"></param>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //private string post<T>(string url, T t, bool json=false)
        //{
        //    RequestBase requestBase = parseMapParams(t);
        //    requestBase.Url = url;
        //    string result =post( requestBase);
        //    LogAgent.Info("api系统返回结果:" + result);
        //    return result;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //private RequestBase parseMapParams<T>(T obj)
        //{
        //    RequestBase map = new RequestBase();
        //    string result = JsonConvert.SerializeObject(obj);
        //    JObject jsonObject = (JObject)JsonConvert.DeserializeObject(result);
        //    return map;
        //}

    }
}
