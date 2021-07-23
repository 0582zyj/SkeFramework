using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetSerialPort.Protocols.Constants;
using Newtonsoft.Json;
using SkeFramework.NetSocket.Topology.Nodes;

namespace SkeFramework.NetSerialPort.Topology.Nodes
{
    /// <summary>
    /// 服务节点信息
    /// </summary>
    public class Node : INode
    {

        public Node()
        {
        }
        /// <summary>
        /// 节点版本信息
        /// </summary>
        public NodeVersion nodeVersion { get; set; }
        /// <summary>
        /// 节点参数
        /// </summary>
        public NodeConfig nodeConfig { get; set; }
        /// <summary>
        /// 节点上次访问的时间戳
        /// </summary>
        public long LastPulse { get; set; }
        /// <summary>
        /// 参数Json字节
        /// </summary>
        public string CustomData { get; set; }
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        public string TaskTag { get; set; }
        /// <summary>
        /// 通信类型
        /// </summary>
        public ReactorType reactorType { get; set; }
        /// <summary>
        /// 终端节点
        /// </summary>
        public object EndNodePoint { get; set; }

        public virtual T ToEndPoint<T>()
        {
            if (this.EndNodePoint is T)
            {
                return (T)EndNodePoint;
            }
            return default(T);
        }

        public object Clone()
        {
            return new Node
            {
                CustomData = this.CustomData,
                nodeConfig = this.nodeConfig,
                TaskTag = this.TaskTag,
                nodeVersion=this.nodeVersion,
                LastPulse = DateTime.UtcNow.Ticks,
                EndNodePoint=this.EndNodePoint
            };
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", nodeConfig.ToString(), TaskTag == null ? "none" : TaskTag.ToString());
        }

        #region Static methods

        public static INode Empty()
        {
            return new Node();
        }
        #endregion


    }
}