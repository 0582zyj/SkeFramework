using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MicrosServices.API.PermissionSystem.MiddleWares
{
    /// <summary>
    /// 自定义中间件实现类
    /// </summary>
    public class RequestCultureMiddleware
    {
        //using Microsoft.AspNetCore.Http
        private readonly RequestDelegate _next;


        /// <summary>
        /// 程序启动时调用
        /// </summary>
        /// <param name="next"></param>
        public RequestCultureMiddleware(RequestDelegate next)
        {
            this._next = next;
        }


        /// <summary>
        　　　　　　///每个页面请求时自动调用，方法按约定命名，必需是Invoke或InvokeAsync
        　　　　　　/// </summary>
        　　　　　　/// <param name="context"></param>
        　　　　　　/// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                //using System.Globalization;
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

    /// <summary>
    /// 通过扩展方法公开中间件 
    /// </summary>
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            //在管道中添加一个use的中间件
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
