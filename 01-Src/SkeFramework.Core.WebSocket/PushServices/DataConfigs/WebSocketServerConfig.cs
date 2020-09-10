using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushServer
{
    /// <summary>
    /// 服务端核心配置
    /// </summary>
    public class WebSocketServerConfig : WebSocketClientConfig
    {
        /// <summary>
        /// 设置当前服务端
        /// </summary>
        public string ServerPath { get; set; }
    }
}
