using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices.PushBrokers
{
    /// <summary>
    /// 推送接口
    /// </summary>
    interface IPushBroker
    {
        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="receiveClientId">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        void SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, object message, bool receipt = false);
    }
}
