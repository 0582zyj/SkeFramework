using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.WebSockets;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushClients
{
    /// <summary>
    /// 客户端链接信息类
    /// </summary>
    public class WebSocketSession
    {
        public WebSocket SocketClient;
        /// <summary>
        /// 客户端唯一标识符
        /// </summary>
        public Guid SessionId;

        public WebSocketSession(WebSocket socket, Guid clientId)
        {
            this.SocketClient = socket;
            this.SessionId = clientId;
        }
    }
}
