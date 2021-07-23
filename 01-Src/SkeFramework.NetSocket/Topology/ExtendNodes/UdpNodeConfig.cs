using SkeFramework.NetSerialPort.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Topology.ExtendNodes
{
    /// <summary>
    /// UDP通信参数
    /// </summary>
    public class UdpNodeConfig : NodeConfig
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string LocalAddress { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int LocalPort { get; set; }
        /// <summary>
        /// Mac地址
        /// </summary>
        public string MacAddress{ get; set; }

        public UdpNodeConfig()
        {
            SpitChar = "-";
            Prefix = "udp";
        }

        public override string ToString()
        {
            List<string> paraList = new List<string>();
            paraList.Add(LocalAddress);
            paraList.Add(LocalPort.ToString());
            paraList.Add(MacAddress.ToString());
            return String.Format("{0}://{1}",Prefix, String.Join(SpitChar,paraList));
        }
    }
}
