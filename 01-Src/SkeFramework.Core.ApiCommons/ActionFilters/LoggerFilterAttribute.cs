using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SkeFramework.Core.NetLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.ApiCommons.Filter
{
    /// <summary>
    /// 日志输出过滤器
    /// </summary>
    public class LoggerFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        private string MoudleName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        private string MethodName { get; set; }
        /// <summary>
        /// 构造日志类型
        /// </summary>
        /// <param name="Moudle">模块名称</param>
        /// <param name="Method">方法名称</param>
        public LoggerFilterAttribute(string Moudle="", string Method = "")
        {
            this.MoudleName = Moudle;
            this.MethodName = Method;
        }

        /// <summary>
        /// 执行方法前先执行这
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            try
            {
                string Moudle = String.Format("接收[{0}-{1}]请求 {2}", MoudleName, MethodName, context.ActionDescriptor.DisplayName);
                string RequestAddress = String.Format("RequestAddress:       {0}:{1}", context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Connection.RemotePort.ToString());
                string URL = String.Format("uri:                  {0}", context.HttpContext.Request.Path.Value.ToString());
                string method = String.Format("method:               {0}", context.HttpContext.Request.Method.ToString());
                string contentType = String.Format("contentType:          {0}", context.HttpContext.Request.ContentType == null ? "null" : context.HttpContext.Request.ContentType.ToString());
                string parameter = String.Format("parameter:            {0}", JsonConvert.SerializeObject(context.ActionArguments));
                StringBuilder message = new StringBuilder();
                message.AppendLine(Moudle);
                message.AppendLine(RequestAddress);
                message.AppendLine(URL);
                message.AppendLine(method);
                message.AppendLine(contentType);
                message.AppendLine(parameter);
                LogAgent.Info(message.ToString());
                CounterToken counterToken = LogAgent.StartCounter();
                context.ActionDescriptor.Properties.TryAdd("Action-" + context.HttpContext.TraceIdentifier, counterToken);
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 执行方法后执行这
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            try
            {
                object obj = null;
                bool result = context.ActionDescriptor.Properties.TryGetValue("Action-" + context.HttpContext.TraceIdentifier, out obj);
                if (result && obj is CounterToken)
                {
                    CounterToken counterToken = (CounterToken) obj;
                    LogAgent.StopCounterAndLog(counterToken, String.Format("结束[{0}-{1}]请求{2}", MoudleName, MethodName, context.ActionDescriptor.DisplayName));
                    return;
                }
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
        }
    }

}
