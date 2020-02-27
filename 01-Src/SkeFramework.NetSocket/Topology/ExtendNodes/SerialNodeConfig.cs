using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology.ExtendNodes
{
    /// <summary>
    /// 串口通信参数
    /// </summary>
    public class SerialNodeConfig:NodeConfig
    {
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

        public SerialNodeConfig()
        {
            SpitChar = "-";
            Prefix = "udp";
            PortName = string.Empty;
        }
      
        public override string ToString()
        {
            List<string> paraList = new List<string>();
            paraList.Add(PortName.ToString());
            paraList.Add(BaudRate.ToString());
            paraList.Add(DataBits.ToString());
            paraList.Add(StopBits.ToString());
            paraList.Add(Parity.ToString());
            return String.Format("{0}://{1}", Prefix, String.Join(SpitChar, paraList));
        }
    }
}
