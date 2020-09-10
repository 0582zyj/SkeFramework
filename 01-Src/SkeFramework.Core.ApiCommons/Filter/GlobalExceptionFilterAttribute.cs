using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SkeFramework.Core.ApiCommons.Exceptions;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Network.Responses;
using System;
using System.Net.WebSockets;

namespace SkeFramework.Core.ApiCommons.Filter
{
    /// <summary>
    /// 全局异常处理过滤器
    /// </summary>
    public class GlobalExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            JsonResponses jsonResponses = JsonResponses.Failed.Clone();
            Exception ex = context.Exception;
            string errMsg = "Exception：" + ex.ToString();
            if (context.Exception.GetType() == typeof(ErrorCodeException))
            {
                ErrorCodeException errorCodeException = (ErrorCodeException)ex;
                //针对不同的自定义异常，做不同处理
                jsonResponses.code = errorCodeException.GetErrorCode();
                jsonResponses.msg = errorCodeException.GetErrorMsg();
              
            }
            else  if (context.Exception.GetType() == typeof(WebSocketException))
            {
                //针对不同的自定义异常，做不同处理
                WebSocketException errorCodeException = (WebSocketException)ex;
                jsonResponses.code = errorCodeException.ErrorCode;
                jsonResponses.msg = errorCodeException.Message;
            }
            if (context.Exception.GetType() == typeof(ArgumentException))
            {
                //针对不同的自定义异常，做不同处理
                ArgumentException errorCodeException = (ArgumentException)ex;          
                jsonResponses.msg = errorCodeException.Message;
            }
            else
            {
                jsonResponses.msg = "系统错误";
            }
            context.Result = new JsonResult(jsonResponses);
            context.ExceptionHandled = true;
            LogAgent.Error(errMsg+" "+JsonConvert.SerializeObject(jsonResponses));
        }
    }
}
