using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Channels;
using SkeFramework.NetSocket.Net;
using SkeFramework.NetSocket.Serialization;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Reactor
{
    /// <summary>
    /// 网络连接接口
    /// </summary>
    //[Obsolete("Use IChannel instead")]
    public interface IReactor : IDisposable
    {
        #region 反射事件
        /// <summary>
        /// 连接事件
        /// </summary>
        event ConnectionEstablishedCallback OnConnection;
        /// <summary>
        /// 接受事件
        /// </summary>
        event ReceivedDataCallback OnReceive;
        /// <summary>
        /// 连接终止事件
        /// </summary>
        event ConnectionTerminatedCallback OnDisconnection;
        /// <summary>
        /// 连接异常事件
        /// </summary>
        event ExceptionCallback OnError;
        #endregion

        #region 字节操作
        /// <summary>
        /// 编码接口
        /// </summary>
        IMessageEncoder Encoder { get; }
        /// <summary>
        /// 解码接口
        /// </summary>
        IMessageDecoder Decoder { get; }
        /// <summary>
        /// 字节缓冲操作器
        /// </summary>
        IByteBufAllocator Allocator { get; }
        #endregion

        /// <summary>
        /// 连接适配器
        /// </summary>
        IConnection ConnectionAdapter { get; }
        NetworkEventLoop EventLoop { get; }
        /// <summary>
        /// 挂起连接的积压允许底层传输
        /// </summary>
        int Backlog { get; set; }
        /// <summary>
        /// 连接是否活跃
        /// </summary>
        bool IsActive { get; }
        /// <summary>
        /// 是否释放
        /// </summary>
        bool WasDisposed { get; }
        /// <summary>
        /// 本地连接
        /// </summary>
        IPEndPoint LocalEndpoint { get; }
        /// <summary>
        /// 连接类型
        /// </summary>
        TransportType Transport { get; }

        /// <summary>
        /// 连接参数配置
        /// </summary>
        /// <param name="config"></param>
        void Configure(IConnectionConfig config);
        #region Socket发送接收数据
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        void Send(NetworkData data);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="destination"></param>
        void Send(byte[] buffer, int index, int length, INode destination);
        #endregion
        #region Socket启动和停止
        /// <summary>
        /// 开始
        /// </summary>
        void Start();
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
        #endregion
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing);
    }
}
