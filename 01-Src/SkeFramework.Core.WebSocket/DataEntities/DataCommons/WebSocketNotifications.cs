using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.DataCommons
{
    /// <summary>
    /// WS通知
    /// </summary>
    public class WebSocketNotifications
    {
        /// <summary>
        /// 发送者的客户端id
        /// </summary>
        public Guid SenderSessionId { get; set; }
        /// <summary>
        /// 接收者的客户端id
        /// </summary>
        public List<Guid> ReceiveSessionIds { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否回执
        /// </summary>
        public bool Receipt { get; set; }
        /// <summary>
        /// 消息通知标识
        /// </summary>
        public object NotificationTag { get; set; }

        public WebSocketNotifications()
        {
            this.ReceiveSessionIds = new List<Guid>();
            this.Receipt = false;
        }

        /// <summary>
        /// 检查是否需要回调消息
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool CheckReceipt(Guid sessionId)
        {
            return  sessionId != this.SenderSessionId && this.Receipt;
        }
    }
}
