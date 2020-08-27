using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.ApiCommons.Exceptions
{
    /// <summary>
    /// 错误代号异常
    /// </summary>
    public class ErrorCodeException : Exception
    {
        private readonly int code;
        private readonly string msg;


        public ErrorCodeException(int code,string message):base(message)
        {
            this.code = code;
            this.msg = message;
        }
        /// <summary>
        /// 获取错误代码
        /// </summary>
        /// <returns></returns>
        public int GetErrorCode()
        {
            return this.code;
        }
        /// <summary>
        /// 获取异常提示
        /// </summary>
        /// <returns></returns>
        public string GetErrorMsg()
        {
            return this.msg;
        }
    }
}
