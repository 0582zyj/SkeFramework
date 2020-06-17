using SkeFramework.Push.Core.Interfaces;
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
    public class PushClientConnection : IPushConnection<INotification>
    {
        /// <summary>
        /// 发送链接
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public Task Send(INotification notification)
        {
            return Task.Factory.StartNew(delegate
            {
                if(notification is WebSocketNotifications)
                {
                    WebSocketNotifications webSocket = (WebSocketNotifications)notification;
                    if (webSocket.session != null)
                    {
                        webSocket.session.Send(notification.Message);
                    }
                    else
                    {

                    }
                }
            });
        }
    }
}
