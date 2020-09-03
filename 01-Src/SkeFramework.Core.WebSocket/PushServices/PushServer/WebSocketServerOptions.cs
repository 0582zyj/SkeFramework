using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushServer
{
    /// <summary>
    /// 服务端核心配置
    /// </summary>
    public class WebSocketServerOptions : WebSocketSessionOptions
    {
        /// <summary>
        /// 设置服务名称
        /// </summary>
        public string ServerName { get; set; }
    }
}
