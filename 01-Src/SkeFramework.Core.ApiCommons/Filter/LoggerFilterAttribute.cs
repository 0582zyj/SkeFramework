using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.ApiCommons.Filter
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
            try
            {
                
            }
            catch //(Exception ex)
            {
            }
        }
    }

}
