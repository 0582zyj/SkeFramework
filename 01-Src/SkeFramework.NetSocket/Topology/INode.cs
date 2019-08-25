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
        /// IP地址
        /// </summary>
        IPAddress Host { get; set; }
        /// <summary>
        /// 节点端口号
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        TransportType TransportType { get; set; }

        /// <summary>
        ///     The name of this machine
        /// </summary>
        string MachineName { get; set; }

        /// <summary>
        /// 操作熊
        /// </summary>
        string OS { get; set; }

        /// <summary>
        /// 服务运行版本
        /// </summary>
        string ServiceVersion { get; set; }

        /// <summary>
        /// Json字节数据
        /// </summary>
        string CustomData { get; set; }

        /// <summary>
        /// 将节点信息转为Socket监听点
        /// </summary>
        IPEndPoint ToEndPoint();
    }
}
