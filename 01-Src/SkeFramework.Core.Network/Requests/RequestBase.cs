using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.Requests
{
   public class RequestBase
    {
        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }
        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private Dictionary<string, string> HeaderValue = new Dictionary<string, string>();
        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        public SortedDictionary<string, object> ParameterValue = new SortedDictionary<string, object>();
        public void SetValue(string key, object value,bool IsHeader=false)
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
        public Dictionary<string, string> GetHeaderValue
        {
        get { return this.HeaderValue; }
        set { this.HeaderValue = value; }
        }
        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
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
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            string buff = this.Url.TrimEnd('?')+"?";
            foreach (KeyValuePair<string, object> pair in ParameterValue)
            {
                if (pair.Value == null)
                {
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

    }
}

