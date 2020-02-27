using SkeFramework.NetSocket.Net;
using SkeFramework.NetSocket.Topology.Nodes;
using SkeFramework.NetSocket.Topology.ExtendNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    /// 节点信息扩展方法
    /// </summary>
    public static class NodeExtensions
    {
        public static INode ToNode(this IPEndPoint endPoint, ReactorType transportType)
        {
            NodeConfig nodeConfig = new UdpNodeConfig() {
                LocalAddress= endPoint.Address.ToString(),
                LocalPort= endPoint.Port
            };
            return new Node { nodeConfig= nodeConfig, reactorType = transportType };
        }
     

        public static bool IsEmpty(this INode node)
        {
            return node == Node.Empty();
        }
    }
}
