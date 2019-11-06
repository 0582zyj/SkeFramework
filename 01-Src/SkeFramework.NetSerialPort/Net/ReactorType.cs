using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Net
{
    /// <summary>
    /// 通信类型
    /// </summary>
    public enum ReactorType
    {
        //
        // 摘要: 
        //     UDP 传输。
        Udp = 1,
        //
        // 摘要: 
        //     TCP 传输。
        Tcp = 2,
        //
        // 摘要: 
        //     传输是面向连接的，如 TCP。 指定该值的效果与指定 System.Net.TransportType.Tcp 相同。
        ConnectionOriented = 2,
        //
        // 摘要: 
        //     所有传输类型。
        All = 3,
        /// <summary>
        /// 串口
        /// </summary>
        SerialPorts=4


    }
}
