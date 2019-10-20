using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols
{
    /// <summary>
    /// Typed delegate used for handling received data
    /// </summary>
    /// <param name="incomingData">a <see cref="NetworkData"/> instance that contains information that's arrived over the network</param>
    public delegate void ReceivedDataCallback(NetworkData incomingData, IConnection responseChannel);

    public interface IConnection : IDisposable
    {
        event ReceivedDataCallback Receive;
   

        IMessageEncoder Encoder { get; }
        IMessageDecoder Decoder { get; }

        /// <summary>
        /// Used to allocate reusable buffers for network I/O
        /// </summary>
        IByteBufAllocator Allocator { get; }

        DateTimeOffset Created { get; }

        INode RemoteHost { get; }

        INode Local { get; }

        TimeSpan Timeout { get; }

        bool WasDisposed { get; }

        bool Receiving { get; }

        bool IsOpen();

        /// <summary>
        /// The total number of bytes written the network that are available to be read
        /// </summary>
        /// <returns>the number of bytes received from the network that are available to be read</returns>
        int Available { get; }

        /// <summary>
        /// Messages that have not yet been delivered to their intended destination
        /// </summary>
        int MessagesInSendQueue { get; }

        /// <summary>
        /// 选项配置此传输
        /// </summary>
        /// <param name="config">a <see cref="IConnectionConfig"/> instance with the appropriate configuration options</param>
        void Configure(IConnectionConfig config);

        void Open();

        /// <summary>
        /// 开始接收此连接上的数据
        /// Assumes that <see cref="Receive"/> has already been set.
        /// </summary>
        void BeginReceive();

        /// <summary>
        /// 开始接收此连接上的数据
        /// </summary>
        /// <param name="callback">A callback for when data is received</param>
        void BeginReceive(ReceivedDataCallback callback);

        /// <summary>
        /// 停止接收消息，但保持连接打开
        /// </summary>
        void StopReceive();
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
        /// <summary>
        /// 发送方法
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="destination"></param>
        void Send(byte[] buffer, int index, int length, INode destination);
    }
}
