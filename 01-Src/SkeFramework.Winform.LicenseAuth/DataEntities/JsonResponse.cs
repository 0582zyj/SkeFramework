using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataEntities
{
    
   /// <summary>
   /// 请求返回类
   /// </summary>
    public class JsonResponse:ICloneable
    {
        public static int SuccessCode = 200;
        public static int FailedCode = 400;
        /// <summary>
        /// 成功
        /// </summary>
        public static JsonResponse Success = new JsonResponse(SuccessCode, "成功");
        /// <summary>
        /// 失败
        /// </summary>
        public static JsonResponse Failed = new JsonResponse(FailedCode, "失败");

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

        public JsonResponse() { }
        public JsonResponse(object obj) : this(Success.code, Success.msg, obj) { }
        public JsonResponse(int code, string msg) : this(code, msg, null)
        {
        }

        public JsonResponse(int code, string msg, object data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }

        public bool ValidateResponses()
        {
            return this.code == JsonResponse.SuccessCode;
        }

        public object Clone()
        {
            return new JsonResponse(this.code, this.msg, this.data);
        }
    }
}
