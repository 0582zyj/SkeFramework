﻿using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushEvent
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
                SenderSessionId = senderClientId,
                Message = message,
                Receipt = receipt,
                ReceiveSessionIds = new List<Guid>()
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
            if (this.Notifications.ReceiveSessionIds == null)
            {
                this.Notifications.ReceiveSessionIds = new List<Guid>();
            }
            if (!this.Notifications.ReceiveSessionIds.Contains(receiveClientId))
            {
                this.Notifications.ReceiveSessionIds.Add(receiveClientId);
            }
        }
    }
}
