using Microsoft.AspNetCore.Builder;
using SkeFramework.Core.WebSocketPush.PushServices.PushServer;
using System;

namespace SkeFramework.Core.WebSocketPush
{
    public static class WebSocketServerExtenssions
    {
        static bool isUseWebSockets = false;

        /// <summary>
        /// 启用 ImServer 服务端
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder app, WebSocketServerOptions options)
        {
            app.Map(options.PathMatch, appcur =>
            {
                var imserv = new WebSocketServerBroker(options);
                if (isUseWebSockets == false)
                {
                    isUseWebSockets = true;
                    appcur.UseWebSockets();
                }
                appcur.Use((ctx, next) =>
                    imserv.Acceptor(ctx, next));
            });
           
            return app;
        }
    }
}
