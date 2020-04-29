using SkeFramework.NetSocket.Net;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    /// 通信节点配置
    /// </summary>
    public class NodeConfig
    {
        public NodeConfig()
        {
            SpitChar = "-";
        }
        /// <summary>
        /// 地址分隔符
        /// </summary>
        public string SpitChar { get; set; }
        /// <summary>
        /// 地址前缀
        /// </summary>
        public string Prefix { get; set; }
    }
}
