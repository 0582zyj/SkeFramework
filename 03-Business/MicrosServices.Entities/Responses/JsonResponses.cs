using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Responses
{
    /// <summary>
    /// 请求返回类
    /// </summary>
    public class JsonResponses
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static  JsonResponses Success = new JsonResponses(200, "成功");
        /// <summary>
        /// 失败
        /// </summary>
        public static JsonResponses Failed = new JsonResponses(400, "失败");

        /// <summary>
        /// 请求应答Code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public object Data { get; set; }

        public JsonResponses(object obj) : this(Success.Code, Success.Msg, obj) { }

        public JsonResponses(int code, string msg) : this(code, msg, null)
        {
        }

        public JsonResponses(int code, string msg, object data)
        {
            this.Code = code;
            this.Msg = msg;
            this.Data = data;
        }

    }
}
