using SkeFramework.NetSerialPort.Net;
using SkeFramework.NetSocket.Topology.Nodes;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Topology
{
    /// <summary>
    /// 连接节点接口
    /// </summary>
    public interface INode : ICloneable
    {        
        /// <summary>
        /// 节点版本信息
        /// </summary>
        NodeVersion nodeVersion { get; set; }
        /// <summary>
        /// Json字节数据
        /// </summary>
        string CustomData { get; set; }
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        string TaskTag { get; set; }
        /// <summary>
        /// 通信类型
        /// </summary>
        ReactorType reactorType { get; set; }
        /// <summary>
        /// 链接参数
        /// </summary>
        NodeConfig nodeConfig { get; set; }
        /// <summary>
        /// 终端节点
        /// </summary>
        object EndNodePoint { get; set; }
        /// <summary>
        /// 将节点信息转为监听点
        /// </summary>
        T ToEndPoint<T>();
    }
}
