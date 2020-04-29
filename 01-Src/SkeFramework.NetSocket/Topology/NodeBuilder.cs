using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkeFramework.NetSocket.Net;
using SkeFramework.NetSocket.Topology.Nodes;
using SkeFramework.NetSocket.Topology.ExtendNodes;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    /// 节点构建器类
    /// </summary>
    public static class NodeBuilder
    {
        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <returns>A new INode instance</returns>
        public static INode BuildNode()
        {
            var n = new Node
            {
                LastPulse = DateTime.UtcNow.Ticks,
                nodeConfig = new NodeConfig()
            };
            return n;
        }

        /// <summary>
        /// 注入IP地址
        /// </summary>
        /// <param name="n">节点对象</param>
        /// <param name="host">IP地址</param>
        public static INode Host(this INode n, ReactorType hostType)
        {
            n.reactorType = hostType;
            switch (hostType)
            {
                case ReactorType.SerialPorts:
                    n.nodeConfig = JsonConvert.DeserializeObject<SerialNodeConfig>(n.CustomData);
                    break;
                case ReactorType.Udp:
                    n.nodeConfig = JsonConvert.DeserializeObject<UdpNodeConfig>(n.CustomData);
                    break;
            }
            return n;
        }


        /// <summary>
        ///  注入机器名
        /// </summary>
        /// <param name="n">节点名</param>
        /// <param name="machineName">机器名称</param>
        public static INode MachineName(this INode n, string machineName)
        {
            n.MachineName = machineName;
            return n;
        }

        /// <summary>
        /// 注入系统名称
        /// </summary>
        /// <param name="n">节点名</param>
        /// <param name="osName">系统名称</param>
        public static INode OperatingSystem(this INode n, string osName)
        {
            n.OS = osName;
            return n;
        }

        /// <summary>
        /// 注入版本号
        /// </summary>
        /// <param name="n">节点</param>
        /// <param name="serviceVersion">节点版本号</param>
        public static INode WithVersion(this INode n, string serviceVersion)
        {
            n.ServiceVersion = serviceVersion;
            return n;
        }

        /// <summary>
        ///  注入传输数据
        /// </summary>
        public static INode WithCustomData(this INode n, string customData)
        {
            n.CustomData = customData;
            return n;
        }


    }
}
