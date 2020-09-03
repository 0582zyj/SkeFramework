using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.DataCommons
{
    /// <summary>
    /// WS消息实体
    /// </summary>
   public class WebSocketNotifications
    {
        /// <summary>
        /// 发送者的客户端id
        /// </summary>
        public Guid SenderClientId { get; set; }
        /// <summary>
        /// 接收者的客户端id
        /// </summary>
        public List<Guid> ReceiveClientId { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否回执
        /// </summary>
        public bool Receipt { get; set; }

        public WebSocketNotifications()
        {
            this.ReceiveClientId = new List<Guid>();
        }

        /// <summary>
        /// 检查是否需要回调消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool CheckReceipt(Guid clientId)
        {
            return this.SenderClientId != Guid.Empty && clientId != this.SenderClientId && this.Receipt;
        }
    }
}
