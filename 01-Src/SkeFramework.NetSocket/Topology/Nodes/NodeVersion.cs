using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology.Nodes
{
    /// <summary>
    /// 节点信息
    /// </summary>
   public class NodeVersion
    {
        /// <summary>
        /// 机器名
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public string OS { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string ServiceVersion { get; set; }
    }
}
