using Microsoft.AspNetCore.Builder;
using SkeFramework.Core.WebSocketPush.PushServices.PushServer;
using System;
using System.Diagnostics;

namespace SkeFramework.Core.WebSocketPush
{
    /// <summary>
    /// 服务端启动配置
    /// </summary>
    public static class WebSocketServerExtenssions
    {
        static bool isUseWebSockets = false;

        /// <summary>
        /// 启用WebSocketServer服务端
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder app, WebSocketServerConfig options)
        {
            try
            {
                options.PathMatch =String.IsNullOrEmpty(options.PathMatch)?"/": $"/{options.PathMatch}";
                app.Map(options.PathMatch, appcur =>
                {
                    var SocketServer = new WebSocketServerBroker(options);
                    if (isUseWebSockets == false)
                    {
                        isUseWebSockets = true;
                        appcur.UseWebSockets();
                    }
                    appcur.Use((context, next) =>SocketServer.Acceptor(context, next));
                });
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return app;
        }

        /// <summary>
        /// 启用WebSocketServer服务端
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder app, WebSocketServerBroker SocketServer)
        {
            try
            {
                app.Map(SocketServer._pathMatch, appcur =>
                {
                    if (isUseWebSockets == false)
                    {
                        isUseWebSockets = true;
                        appcur.UseWebSockets();
                    }
                    appcur.Use((context, next) => SocketServer.Acceptor(context, next));
                });
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return app;
        }
    }
}
