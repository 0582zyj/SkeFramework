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
using SkeFramework.Core.ApiCommons.DataUtil;
using SkeFramework.Core.WebSocketPush;
using SkeFramework.Core.WebSocketPush.PushServices.PushServer;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSocketServer(new WebSocketServerConfig
            {
                Redis = new CSRedis.CSRedisClient(Configuration["WebSocketServer:CSRedisClient"]),
                Servers = Configuration["WebSocketServer:Servers"].Split(",").ToList(), //集群配置
                ServerBasePath = Configuration["WebSocketServer:Server"],
                PathMatch= Configuration["WebSocketServer:WsPath"],
            });
       
            app.UseMvc();
        }
    }
}
