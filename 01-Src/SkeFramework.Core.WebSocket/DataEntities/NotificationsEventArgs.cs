using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities
{
    /// <summary>
    /// 消息通知事件
    /// </summary>
    public class NotificationsEventArgs : EventArgs
    {
        /// <summary>
        /// WS消息实体
        /// </summary>
       public WebSocketNotifications Notifications { get; set; }
        /// <summary>
        /// 服务器节点
        /// </summary>
        public string Server { get; }

        internal NotificationsEventArgs(string server, Guid senderClientId, string message, bool receipt = false)
            :this(server, new WebSocketNotifications()
            {
                SenderClientId = senderClientId,
                Message = message,
                Receipt = receipt,
                ReceiveClientId = new List<Guid>()
            })
        {
        }

        internal NotificationsEventArgs(string server , WebSocketNotifications Notifications)
        {
            this.Server = server;
            this.Notifications = Notifications;

        }
        /// <summary>
        /// 新增接受客户端ID
        /// </summary>
        /// <param name="receiveClientId"></param>
        public void AddReceiveClientId(Guid receiveClientId)
        {
            if (this.Notifications == null)
                return;
            if (this.Notifications.ReceiveClientId == null)
            {
                this.Notifications.ReceiveClientId = new List<Guid>();
            }
            if (!this.Notifications.ReceiveClientId.Contains(receiveClientId))
            {
                this.Notifications.ReceiveClientId.Add(receiveClientId);
            }
        }
    }
}
