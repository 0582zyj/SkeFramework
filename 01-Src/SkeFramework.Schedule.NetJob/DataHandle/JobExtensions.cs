using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SkeFramework.Schedule.NetJob.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.DataHandle
{
   /// <summary>
   /// 
   /// </summary>
    public static class JobExtensions
    {
        /// <summary>
        /// Add the CrontabJob service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddCrontabJob(this IServiceCollection services, Action<ServerOptionsBuilder> options = null)
        {
            var builder = new ServerOptionsBuilder();
            options?.Invoke(builder);
            builder.BuildServices(services);
            return services;
        }

        /// <summary>
        /// Use the CrontabJob
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCrontabJob(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.GetRequiredService<ServerBootstrap>();
            return builder;
        }
    }
}
