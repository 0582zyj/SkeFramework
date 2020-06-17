using SkeFramework.Push.Core.Interfaces;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.DataEntities
{
    public class WebSocketNotifications : INotification
    {
        /// <summary>
        /// 客户端连接属性
        /// </summary>
        public WebSocketSession session;
        /// <summary>
        /// 标识
        /// </summary>
        public object Tag { get => session.SessionID; }
        /// <summary>
        /// 是否已注册
        /// </summary>
        /// <returns></returns>
        public bool IsDeviceRegistrationIdValid()
        {
            return true;
        }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Message { get; set; }

        public WebSocketNotifications(WebSocketSession socketSession)
        {
            session = socketSession;
        }
    }
}
