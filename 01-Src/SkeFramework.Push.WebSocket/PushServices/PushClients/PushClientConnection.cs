using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap.Factorys.Abstracts;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Listenser.ChannelListensers;
using SkeFramework.Push.WebSocket.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.PushServices.PushClients
{
    /// <summary>
    /// 推送客户端链接
    /// </summary>
    public class PushClientConnection: DefaultChannelPromise, IPushConnection<WebSocketNotifications>
    {
        public string GetTag()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发送链接
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public Task Send(WebSocketNotifications notification)
        {
            return Task.Factory.StartNew(delegate
            {
                WebSocketNotifications webSocket = (WebSocketNotifications)notification;
                if (webSocket.session != null)
                {
                    webSocket.session.Send(notification.Message);
                }
                else
                {

                }
            });
        }

    }
}
