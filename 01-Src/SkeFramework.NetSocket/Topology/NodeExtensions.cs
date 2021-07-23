using SkeFramework.NetSerialPort.Net;
using SkeFramework.NetSerialPort.Topology.Nodes;
using SkeFramework.NetSerialPort.Topology.ExtendNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkeFramework.NetSerialPort.Topology
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

        public static object GetNodeConfigValue(this INode node,string PropertName)
        {
            JObject jObject = JObject.Parse(node.CustomData);
            return jObject[PropertName];
        }

        public static INode SetNodeConfigValue(this INode node, string PropertName,object PropertValue)
        {
            JObject jObject = JObject.Parse(node.CustomData);
            jObject[PropertName]= JToken.FromObject(PropertValue);
            node.CustomData = JsonConvert.SerializeObject(jObject);
            node.Host(node.reactorType);
            return node;
        }
    }
}
