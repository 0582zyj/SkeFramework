using SkeFramework.Core.WebSocketPush.PushServices.PushServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.API.RealTimeSystem.Handle
{
    public class WebSocketServerHandle
    {
        private WebSocketServerConfig ServerConfig;

        public WebSocketServerHandle(string CSRedisClientURL,string Server,string WsPath)
        {
            ServerConfig = new WebSocketServerConfig()
            {
                Redis = new CSRedis.CSRedisClient(CSRedisClientURL),
                ServerPath = Server,
                PathMatch = WsPath,
            };
        }
        public WebSocketServerHandle(WebSocketServerConfig serverConfig)
        {
            ServerConfig = serverConfig;
        }

        public WebSocketServerBroker NewWebSocketServer()
        {
            WebSocketServerBroker ServerBroker = new WebSocketServerBroker(this.ServerConfig);
            ServerBroker.NewSessionConnected += ServerBroker_NewSessionConnected;
            ServerBroker.NewMessageReceived += ServerBroker_NewMessageReceived;
            return ServerBroker;
        }

        private void ServerBroker_NewMessageReceived(SkeFramework.Core.WebSocketPush.PushServices.PushClients.WebSocketSession session, string value)
        {
        }

        private void ServerBroker_NewSessionConnected(SkeFramework.Core.WebSocketPush.PushServices.PushClients.WebSocketSession session, Guid value)
        {
        }
    }

}
