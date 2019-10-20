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
    }
}
