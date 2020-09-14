using SkeFramework.Core.WebSocketPush.PushServices.PushServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.API.RealTimeSystem.Handle
{
    public class WebSocketServerHandle
    {
        public const int DefaultTimeOutSecond = 0;

        /// <summary>
        /// 服务端配置信息
        /// </summary>
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

        /// <summary>
        /// 创建一个推送服务端
        /// </summary>
        /// <returns></returns>
        public WebSocketServerBroker NewWebSocketServer()
        {
            WebSocketServerBroker ServerBroker = new WebSocketServerBroker(this.ServerConfig);
            ServerBroker.NewSessionConnected += ServerBroker_NewSessionConnected;
            ServerBroker.NewMessageReceived += ServerBroker_NewMessageReceived;
            return ServerBroker;
        }
        /// <summary>
        /// 新消息处理
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private void ServerBroker_NewMessageReceived(SkeFramework.Core.WebSocketPush.PushServices.PushClients.WebSocketSession session, string value)
        {
        }
        /// <summary>
        /// 新链接处理
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private void ServerBroker_NewSessionConnected(SkeFramework.Core.WebSocketPush.PushServices.PushClients.WebSocketSession session, Guid value)
        {
        }


    }
}
