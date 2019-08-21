using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    /// 连接节点接口
    /// </summary>
    public interface INode : ICloneable
    {
        /// <summary>
        ///     The IP address of this seed
        /// </summary>
        IPAddress Host { get; set; }

        /// <summary>
        ///     The port number of this node's capability
        /// </summary>
        int Port { get; set; }

        /// <summary>
        ///     The connection type used by this node
        /// </summary>
        TransportType TransportType { get; set; }

        /// <summary>
        ///     The name of this machine
        /// </summary>
        string MachineName { get; set; }

        /// <summary>
        ///     OS name and version of this machine
        /// </summary>
        string OS { get; set; }

        /// <summary>
        ///     version of the service running on this node
        /// </summary>
        string ServiceVersion { get; set; }

        /// <summary>
        ///     A JSON blob representing arbitrary data about this node
        /// </summary>
        string CustomData { get; set; }

        /// <summary>
        ///     Converts the node to an <see cref="IPEndPoint" />
        /// </summary>
        IPEndPoint ToEndPoint();
    }
}
