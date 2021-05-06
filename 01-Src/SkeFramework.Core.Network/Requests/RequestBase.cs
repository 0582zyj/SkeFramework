using Newtonsoft.Json;
using SkeFramework.Core.Network.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.Requests
{
    /// <summary>
    /// 请求基类
    /// </summary>
    public class RequestBase: ICloneable
    {
        public static readonly RequestBase Get = new RequestBase(ContentTypeEnums.GETFORM);
        public static readonly RequestBase PostForm = new RequestBase(ContentTypeEnums.POSTFORM);
        public static readonly RequestBase PostJson = new RequestBase(ContentTypeEnums.POSTJSON);
        public RequestBase()
        {
        }

        public RequestBase(ContentTypeEnums contentType)
        {
            this.contentType = contentType;
            switch (this.contentType)
            {
                case ContentTypeEnums.GETFORM:
                    this.Method = HttpMethod.Get.Method.ToString();
                    break;
                default:
                    this.Method = HttpMethod.Post.Method.ToString();
                    break;
            }
        }
        public RequestBase(string Method, ContentTypeEnums contentType)
        {
            this.Method = Method;
            this.contentType = contentType;
        }
       
        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public ContentTypeEnums contentType { get; set; }
        /// <summary>
        /// Head
        /// </summary>
        public Dictionary<string, string> HeaderValue = new Dictionary<string, string>();
        /// <summary>
        /// 参数
        /// </summary>
        public SortedDictionary<string, object> ParameterValue = new SortedDictionary<string, object>();
        /// <summary>
        /// 设置Json数据
        /// </summary>
        /// <param name="value"></param>
        public void SetJsonValue(object value)
        {
            this.SetValue("body", value);
        }
        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="IsHeader"></param>
        public void SetValue(string key, object value, bool IsHeader = false)
        {
            if (IsHeader)
            {
                HeaderValue[key] = value.ToString();
            }
            else
            {
                ParameterValue[key] = value;
            }
        }
        /// <summary>
        /// 根据字段名获取某个字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="IsHeader"></param>
        /// <returns></returns>
        public object GetValue(string key, bool IsHeader)
        {
            object o = null;
            ParameterValue.TryGetValue(key, out o);
            if (IsHeader)
            {
                string value = "";
                HeaderValue.TryGetValue(key, out value);
                o = value;
            }
            else
            {
                ParameterValue.TryGetValue(key, out o);
            }
            return o;
        }
        /// <summary>
        /// 获取Api请求地址【参数拼接】
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            return this.Url.TrimEnd('?') + "?" + this.GetRequestData();
        }
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <returns></returns>
        public string GetRequestData()
        {
            if (contentType == ContentTypeEnums.POSTJSON)
            {
                return JsonConvert.SerializeObject(ParameterValue["body"]);
            }
            string buff = "&";
            foreach (KeyValuePair<string, object> pair in ParameterValue)
            {
                if (pair.Value == null)
                {
                    continue;
                }
                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

       
        
    }
}

