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
        public static int FailedCode = 200;
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

    }
}
