using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Constants;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Net.Reactor
{
    /// <summary>
    /// 网络抽象实现
    /// </summary>
    public abstract class ReactorBase : IReactor
    {

        #region 事件

        public event ReceivedDataCallback OnReceive
        {
            add { }
            // ReSharper disable once ValueParameterNotUsed
            remove { }
        }
        public event SendDataCallback OnSend
        {
            add { }
            remove { }
        }

        #endregion

        #region 构造注入
        protected ReactorBase(INode listener, IMessageEncoder encoder,
            IMessageDecoder decoder, IByteBufAllocator allocator, int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
        {
            Decoder = decoder;
            Encoder = encoder;
            Allocator = allocator;

            Listener = listener;
            Backlog = NetworkConstants.DefaultBacklog;
            BufferSize = bufferSize;
            ConnectionAdapter = new ReactorConnectionAdapter(this);
        }
        #endregion
        /// <summary>
        /// 积压记录
        /// </summary>
        public int Backlog { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public  INode Local { get; set; }
        /// <summary>
        /// 监听者
        /// </summary>
        public readonly INode Listener;
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        protected int BufferSize { get; set; }
        /// <summary>
        /// 编码器
        /// </summary>
        public IMessageEncoder Encoder { get; set; }
        /// <summary>
        /// 解码器
        /// </summary>
        public IMessageDecoder Decoder { get; set; }
        /// <summary>
        /// 缓存区处理程序
        /// </summary>
        public IByteBufAllocator Allocator { get; set; }
        /// <summary>
        /// 接受缓存区
        /// </summary>
        protected NetworkState networkState;
        /// <summary>
        /// 链接适配器
        /// </summary>
        public IConnection ConnectionAdapter { get; set; }
        /// <summary>
        /// 是否活跃
        /// </summary>
        public abstract bool IsActive { get; protected set; }
        /// <summary>
        /// 是否解析数据
        /// </summary>
        public abstract bool IsParsing { get; protected set; }
        /// <summary>
        /// 是否已释放
        /// </summary>
        public bool WasDisposed { get; protected set; }

        public void Start()
        {
            //Don't restart
            if (IsActive) return;
            CheckWasDisposed();
            IsActive = true;
            this.ConnectionAdapter.Open();
            StartInternal();            
        }

        public void Stop()
        {
            CheckWasDisposed();
            try
            {
                IsActive = false;
            }
            catch
            {
            }
            StopInternal();
        }

        public void Send(NetworkData data)
        {
            Send(data.Buffer, 0, data.Length, data.RemoteHost);
        }

        public abstract void Configure(IConnectionConfig config);
        public abstract void Send(byte[] buffer, int index, int length, INode destination);
        protected abstract void StartInternal();
        protected abstract void StopInternal();

        /// <summary>
        /// Abstract method to be filled in by a child class - data received from the
        /// network is injected into this method via the <see cref="NetworkData" /> data type.
        /// </summary>
        /// <param name="availableData">Data available from the network, including a response address</param>
        /// <param name="responseChannel">Available channel for handling network response</param>
        protected virtual void ReceivedData(NetworkData availableData)
        {
            //if (EventLoop.Receive != null)
            //{
            //    EventLoop.Receive(availableData, responseChannel);
            //}
        }


        #region ReactorConnectionAdapter
        protected NetworkState CreateNetworkState(INode socket, INode remotehost)
        {
            IByteBuf byteBuf = Allocator == null ? null : Allocator.Buffer();
            return CreateNetworkState(socket, remotehost, byteBuf, BufferSize);
        }

        protected NetworkState CreateNetworkState(INode socket, INode remotehost, IByteBuf buffer, int bufferSize)
        {
            return new NetworkState(socket, remotehost, buffer, bufferSize);
        }
        /// <summary>
        /// 关闭到远程主机的连接(不关闭服务器)
        /// </summary>
        /// <param name="remoteHost">要终止的远程主机</param>
        internal abstract void CloseConnection(IConnection remoteHost);
        /// <summary>
        /// 关闭到远程主机的连接(不关闭服务器)
        /// </summary>
        /// <param name="reason">关闭原因</param>
        /// <param name="remoteHost">要终止的远程主机</param>
        internal abstract void CloseConnection(Exception reason, IConnection remoteHost);
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Dispose(bool disposing);

        public void CheckWasDisposed()
        {
            if (WasDisposed)
            {
                //throw new SocketConnectionException(ExceptionType.NotOpen, "Already disposed this Reactor");
            }
        }

        #endregion
    }
}
