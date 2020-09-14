using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MicrosServices.API.RealTimeSystem.Handle;
using SkeFramework.Core.ApiCommons.DataUtil;
using SkeFramework.Core.ApiCommons.Filter;
using SkeFramework.Core.ApiCommons.Middlewares;
using SkeFramework.Core.WebSocketPush;
using SkeFramework.Core.WebSocketPush.PushServices.PushServer;
using SkeFramework.Schedule.NetJob.DataHandle;

namespace MicrosServices.API.RealTimeSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton(new ApplicationConfigUtil(Configuration));
            // Filter统一注入MVC框架
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
                //options.Filters.Add(typeof(LoggerFilterAttribute));
            });
            //添加定时任务
            services.AddCrontabJob();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //配置WebSocket
            app.UseWebSocketServer(new WebSocketServerHandle(
                 Configuration["WebSocketServer:CSRedisClient"],
                 Configuration["WebSocketServer:Server"],
                 Configuration["WebSocketServer:WsPath"]
                ).NewWebSocketServer());
            //配置定时任务
            app.UseCrontabJob();
            //配置MVC 
            app.UseMvc();
        }
    }
}
