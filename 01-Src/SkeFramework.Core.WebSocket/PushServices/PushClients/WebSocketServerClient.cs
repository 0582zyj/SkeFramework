using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.WebSockets;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushClients
{
    class WebSocketServerClient
    {
        public WebSocket socket;
        public Guid clientId;

        public WebSocketServerClient(WebSocket socket, Guid clientId)
        {
            this.socket = socket;
            this.clientId = clientId;
        }
    }
}
