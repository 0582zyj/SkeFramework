using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.DataCommons
{
    /// <summary>
    /// 订阅在线统计信息
    /// </summary>
    public class OnlineChannelVo
    {
        /// <summary>
        /// 订阅信息
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 在线任务
        /// </summary>
        public long Online { get; set; }
    }
}
