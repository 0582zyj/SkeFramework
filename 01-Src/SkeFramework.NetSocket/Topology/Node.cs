using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net;
using SkeFramework.Topology;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    /// 服务节点信息
    /// </summary>
    public class Node : INode
    {
        private IPEndPoint _endPoint;

        public Node()
        {
            TransportType = TransportType.Tcp;
        }

        /// <summary>
        /// 节点上次访问的时间戳
        /// </summary>
        public long LastPulse { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddress Host { get; set; }

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

        /// <summary>
        /// Json字节
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// 节点端口
        /// </summary>
        public int Port { get; set; }


        public TransportType TransportType { get; set; }

        public IPEndPoint ToEndPoint()
        {
            return _endPoint ?? (_endPoint = new IPEndPoint(Host, Port));
        }

        public object Clone()
        {
            return new Node
            {
                CustomData = CustomData,
                Host = new IPAddress(Host.GetAddressBytes()),
                MachineName = MachineName,
                Port = Port,
                TransportType = TransportType
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 17;
                hashCode += 23 * Host.GetHashCode();
                hashCode += 23 * Port.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Host, Port);
        }

        #region Static methods

        public static INode Loopback(int port = NetworkConstants.InMemoryPort)
        {
            return NodeBuilder.BuildNode().Host(IPAddress.Loopback).WithPort(port);
        }

        private static readonly INode empty = new EmptyNode();

        public static INode Empty()
        {
            return empty;
        }

        public static INode Any(int port = 0)
        {
            return NodeBuilder.BuildNode().Host(IPAddress.Any).WithPort(port);
        }

        #endregion
    }
}