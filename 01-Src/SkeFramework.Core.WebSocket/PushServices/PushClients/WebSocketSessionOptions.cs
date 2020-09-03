using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushClients
{
    /// <summary>
    /// 推送客户端核心类实现的配置所需
    /// </summary>
    public class WebSocketSessionOptions
    {
        /// <summary>
        /// CSRedis 对象，用于存储数据和发送消息
        /// </summary>
        public CSRedisClient Redis { get; set; }
        /// <summary>
        /// 负载的服务端
        /// </summary>
        public List<string> Servers { get; set; }
        /// <summary>
        /// websocket请求的路径，默认值：/ws
        /// </summary>
        public string PathMatch { get; set; } = "/ws";
    }
}
