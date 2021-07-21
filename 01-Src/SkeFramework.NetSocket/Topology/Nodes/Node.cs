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

        public NodeConfig nodeConfig
        {
            get;
            set;
        }

        public virtual T ToEndPoint<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.CustomData);
        }
        /// <summary>
        /// 节点上次访问的时间戳
        /// </summary>
        public long LastPulse { get; set; }
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
        /// 任务唯一标识
        /// </summary>
        public string TaskTag { get; set; }
        /// <summary>
        /// 通信类型
        /// </summary>
        public ReactorType reactorType { get; set; }

        public object Clone()
        {
            return new Node
            {
                CustomData = CustomData,
                nodeConfig= nodeConfig,
                TaskTag=TaskTag,
                MachineName = MachineName,
                ServiceVersion= ServiceVersion,
                LastPulse=0,
                OS= OS,
            };
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", nodeConfig.ToString(), TaskTag==null?"none": TaskTag.ToString());
        }

        #region Static methods

        public static INode Empty()
        {
            return new Node();
        }
        #endregion


    }
}