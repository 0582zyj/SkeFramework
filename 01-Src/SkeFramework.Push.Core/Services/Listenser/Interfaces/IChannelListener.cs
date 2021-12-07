using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.Push.Core.Listenser.Interfaces
{
    /// <summary>
    /// 监听方法接口
    /// </summary>
    public interface IChannelListener
    {
        /// <summary>
        /// 消息接受
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="taskId"></param>
        void OnReceivedDataPoint(INotification datas, string taskId);
    }
}
