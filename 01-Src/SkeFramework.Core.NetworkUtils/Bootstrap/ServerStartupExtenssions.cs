using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SkeFramework.Core.Common.Networks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.NetworkUtils.Bootstrap
{
    /// <summary>
    /// 服务启动扩展
    /// </summary>
    public static class ServerStartupExtenssions
    {
        /// <summary>
        /// 全局注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalServer(this IApplicationBuilder app, IHostingEnvironment env)
        {
            GlobalContextUtils.ServiceProvider = app.ApplicationServices;
            GlobalContextUtils.HostingEnvironment = env;
            GlobalContextUtils.LogWhenStart(env);
            return app;
        }
        /// <summary>
        /// 注册Session
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSkeSession(this IServiceCollection services)
        {
            //HttpContextAccessor 默认实现了它简化了访问HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //01 - 基于内存的Session
            services.AddDistributedMemoryCache();
            ////03-基于Redis的Session
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost";
            //    options.InstanceName = "SampleInstance";
            //});
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(60 * 24);
            });
            return services;
        }
    }
}
