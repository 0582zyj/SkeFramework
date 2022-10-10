using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.Responses
{
    /// <summary>
    /// 请求返回类
    /// </summary>
    public class JsonResponses
    {
        public static int SuccessCode = 200;
        public static int FailedCode = 400;
        /// <summary>
        /// 成功
        /// </summary>
        public static JsonResponses Success = new JsonResponses(SuccessCode, "成功");
        /// <summary>
        /// 失败
        /// </summary>
        public static JsonResponses Failed = new JsonResponses(FailedCode, "失败");

        /// <summary>
        /// 请求应答Code
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public object data { get; set; }

        public JsonResponses() { }

        public JsonResponses(string ErrorMsg) : this(Failed.code, ErrorMsg) { }
        public JsonResponses(object obj) : this(Success.code, Success.msg, obj) { }
        public JsonResponses(int code, string msg) : this(code, msg, null)
        {
        }

        public JsonResponses(int code, string msg, object data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }

        public bool ValidateResponses()
        {
            return this.code == JsonResponses.SuccessCode;
        }
        /// <summary>
        /// 结果类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetDataValue<T>()
        {
            Type parentType = this.data.GetType();
            Type childType = typeof(T);
            if (parentType == childType || parentType.IsSubclassOf(childType))
            {
                return (T)this.data;
            }
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 结果类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetDataList<T>()
        {
            Type parentType = this.data.GetType();
            Type childType = typeof(T);
            if (parentType == childType || parentType.IsSubclassOf(childType))
            {
                return new List<T>();
            }
            var format = "yyyy-MM-dd hh:mm:ss.fff"; // your datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(data, dateTimeConverter),dateTimeConverter);
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public JsonResponses Clone()
        {
            return new JsonResponses(this.code, this.msg, this.data);
        }

        

    }
}
