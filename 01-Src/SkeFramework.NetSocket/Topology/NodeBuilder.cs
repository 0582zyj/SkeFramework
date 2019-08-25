using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.Topology
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
            var n = new Node { LastPulse = DateTime.UtcNow.Ticks };
            return n;
        }

        /// <summary>
        /// 注入IP地址
        /// </summary>
        /// <param name="n">节点对象</param>
        /// <param name="host">IP地址</param>
        public static INode Host(this INode n, IPAddress host)
        {
            n.Host = host;
            return n;
        }

        /// <summary>
        /// 注入IP地址
        /// </summary>
        /// <param name="n">节点名</param>
        /// <param name="host">IP地址 String格式</param>
        public static INode Host(this INode n, string host)
        {
            IPAddress parseIp;
            if (!IPAddress.TryParse(host, out parseIp))
            {
                var hostentry = Dns.GetHostEntry(host);
                parseIp = hostentry.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                // first IPv4 address
                if (parseIp == null)
                {
                    parseIp = hostentry.AddressList.First(x => x.AddressFamily == AddressFamily.InterNetworkV6);
                    // first IPv6 address
                }
            }

            return Host(n, parseIp);
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
        ///  注入端口
        /// </summary>
        public static INode WithPort(this INode n, int portNum)
        {
            n.Port = portNum;
            return n;
        }

        /// <summary>
        /// 注入传输类型
        /// </summary>
        public static INode WithTransportType(this INode n, TransportType transportType)
        {
            n.TransportType = transportType;
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

        /// <summary>
        /// 根据Socket监听返回节点信息
        /// </summary>
        public static INode FromEndpoint(IPEndPoint endPoint)
        {
            var n = new Node { Host = endPoint.Address, Port = endPoint.Port };
            return n;
        }
    }
}