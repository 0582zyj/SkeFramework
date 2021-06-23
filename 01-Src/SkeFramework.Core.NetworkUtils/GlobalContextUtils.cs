using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.NetLog.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace SkeFramework.Core.Common.Networks
{
    public class GlobalContextUtils
    {
        /// <summary>
        /// 所有已注册的服务和类实例容器。用于依赖项注入。
        /// </summary>
        public static IServiceCollection Services { get; set; }
        /// <summary>
        /// 配置服务提供者。
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// 站点
        /// </summary>
        public static IHostingEnvironment HostingEnvironment { get; set; }

        public static string GetVersion()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            return version.Major + "." + version.Minor;
        }

        /// <summary>
        /// 程序启动时，记录目录
        /// </summary>
        /// <param name="env"></param>
        public static void LogWhenStart(IHostingEnvironment env)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("程序启动");
            sb.AppendLine("ContentRootPath:" + env.ContentRootPath);
            sb.AppendLine("WebRootPath:" + env.WebRootPath);
            sb.AppendLine("IsDevelopment:" + env.IsDevelopment());
            sb.AppendLine("WebVersion:" + GetVersion());
            LogAgent.Write(LogLevel.Info ,sb.ToString());
        }

        /// <summary>
        /// 设置cache control
        /// </summary>
        /// <param name="context"></param>
        public static void SetCacheControl(StaticFileResponseContext context)
        {
            int second = 365 * 24 * 60 * 60;
            context.Context.Response.Headers.Add("Cache-Control", new[] { "public,max-age=" + second });
            context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
        }

    }
}
