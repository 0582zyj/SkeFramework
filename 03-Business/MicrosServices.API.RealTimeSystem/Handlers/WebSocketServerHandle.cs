using MicrosServices.API.RealTimeSystem.DataUtil;
using MicrosServices.Helper.Core.RealTimeSystems.VO;
using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities;
using SkeFramework.Core.WebSocketPush.PushServices.PushEvent;
using SkeFramework.Core.WebSocketPush.PushServices.PushServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.API.RealTimeSystem.Handle
{
    public class WebSocketServerHandle
    {
   

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
            RedisUtil.Redis = ServerConfig.Redis;
        }
        public WebSocketServerHandle(WebSocketServerConfig serverConfig)
        {
            ServerConfig = serverConfig;
            RedisUtil.Redis = ServerConfig.Redis;
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
            ServerBroker.OnServerHandler += new EventHandler<NotificationsEventArgs>(OnServerHandler);
            ServerBroker.OnSend += new EventHandler<NotificationsEventArgs>(OnSendHandle);
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
            ClientVo clientVo = JsonConvert.DeserializeObject<ClientVo>(session.SessionExtraProps);
            string clientRedisKey = RedisUtil.GetUserIdRedisKey(clientVo.AppId,clientVo.UserId);
            ServerConfig.Redis.GetSet(clientRedisKey, session.SessionId.ToString());
        }
        /// <summary>
        /// 传给服务端处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnServerHandler(object sender, NotificationsEventArgs e)
        {

        }
        /// <summary>
        /// 发送消息回调事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSendHandle(object sender, NotificationsEventArgs e)
        {
        }

    }
}
