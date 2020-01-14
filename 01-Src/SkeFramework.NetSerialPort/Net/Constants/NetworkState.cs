using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Net.Constants
{
    /// <summary>
    /// 用于处理连接实例上的数据的状态对象
    /// </summary>
    public class NetworkState
    {
        public NetworkState(SerialPort socket, INode remoteHost, IByteBuf buffer, int rawBufferLength)
        {
            Buffer = buffer;
            RemoteHost = remoteHost;
            Socket = socket;
            RawBuffer = new byte[rawBufferLength];
        }
        /// <summary>
        /// Socket对象
        /// </summary>
        public SerialPort Socket { get; private set; }
        /// <summary>
        /// 远程主机节点
        /// </summary>
        public INode RemoteHost { get; set; }
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public IByteBuf Buffer { get; private set; }
        /// <summary>
        /// 原始缓冲区
        /// </summary>
        public byte[] RawBuffer { get;  set; }
    }
}
