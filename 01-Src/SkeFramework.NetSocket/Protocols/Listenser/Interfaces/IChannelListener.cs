using SkeFramework.NetSerialPort.Protocols.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Protocols.Listenser.Interfaces
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
        void OnReceivedDataPoint(NetworkData datas, string taskId);
    }
}
