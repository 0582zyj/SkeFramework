using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SkeFramework.Core.ApiCommons.Middlewares;

namespace MicrosServices.API.PermissionSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 此方法由运行时调用。使用此方法向容器添加服务。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //添加定时任务
            //services.AddCrontabJob();
            //
        }

        /// <summary>
        /// 此方法由运行时调用。使用此方法配置HTTP请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();//.Run(Invoke)
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            //异常业务处理中间件
            app.UseMiddleware(typeof(ExceptionMiddleWare));
            ////配置定时任务
            //app.UseCrontabJob();
            app.UseAuthentication();
            //配置Http重定向
            app.UseHttpsRedirection();
            //配置MVC路由规则
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }

        private async Task ResponseAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(
                    //打印当前显示的语言
                    $"Hello { CultureInfo.CurrentCulture.DisplayName }"
                    );
        }
    }
}
