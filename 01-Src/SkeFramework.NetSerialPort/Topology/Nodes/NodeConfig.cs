using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Topology
{
    /// <summary>
    /// 串口配置
    /// </summary>
   public class NodeConfig
    {
        public NodeConfig()
        {
            PortName = string.Empty;
        }
        /// <summary>
        /// 端口名
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }
        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity { get; set; }
    }
}
