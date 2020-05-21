using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace SkeFramework.Exceptionless.CoreApi.FilterAttributes
{
    public class LoggerFilterAttribute : ActionFilterAttribute
    {
        private string MoudleName { get; set; }
        private string MethodName { get; set; }
        /// <summary>
        /// 构造日志类型
        /// </summary>
        /// <param name="Moudle">模块名称</param>
        /// <param name="Method">方法名称</param>
        public LoggerFilterAttribute(string Moudle, string Method)
        {
            this.MoudleName = Moudle;
            this.MethodName = Method;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            string Token = null;
            try
            {

                
                ExceptionlessClient.Default.CreateLog("[" + MoudleName + "]-[" + MethodName + "]-[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]", LogLevel.Info)
                                           .SetSource("[" + MoudleName + "]-[" + MethodName + "]-[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]")
                                           .SetMessage("接口参数:" + "")
                                           .SetUserIdentity(Token ?? null)
                                           .SetProperty("Response", JsonConvert.SerializeObject(context.Result))
                                           .AddTags(MoudleName)
                                           .AddTags(MethodName)
                                           .Submit();
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().AddTags(MoudleName)
                                    .AddTags(MethodName)
                                    .SetUserIdentity(Token ?? null)
                                    .Submit();

            }
        }
    }

}
