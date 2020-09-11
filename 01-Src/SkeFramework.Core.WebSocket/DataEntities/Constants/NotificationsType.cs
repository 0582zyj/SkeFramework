using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.Constants
{
    /// <summary>
    /// 通知类型
    /// </summary>
    public enum NotificationsType
    {
        [Description("在线回调通知")]
        receipt_online = 2001,
        [Description("离线回调通知")]
        receipt_offline = 2002,
        [Description("发送完成回调通知")]
        receipt_send = 2003,
    }
}
