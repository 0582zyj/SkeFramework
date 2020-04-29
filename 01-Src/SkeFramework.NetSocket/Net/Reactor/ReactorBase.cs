using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Buffers.Allocators;
using SkeFramework.NetSocket.Net.Constants;
using SkeFramework.NetSocket.Protocols;
using SkeFramework.NetSocket.Protocols.Configs;
using SkeFramework.NetSocket.Protocols.Connections;
using SkeFramework.NetSocket.Protocols.Constants;
using SkeFramework.NetSocket.Protocols.Response;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net.Reactor
{
    /// <summary>
    /// 网络抽象实现
    /// </summary>
    public abstract class ReactorBase : IReactor
    {

        #region 事件

        public event ReceivedDataCallback OnReceive
        {
            add {  }
            // ReSharper disable once ValueParameterNotUsed
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
            //LocalEndpoint = new IPEndPoint(localAddress, localPort);      
            Backlog = NetworkConstants.DefaultBacklog;
            BufferSize = bufferSize;
            ConnectionAdapter = new ReactorConnectionAdapter(this);
        }
        #endregion
        ///// <summary>
        ///// 监听者
        ///// </summary>
        //protected SerialPort Listener;
        /// <summary>
        /// 监听者
        /// </summary>
        protected INode Listener;
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        protected int BufferSize { get; set; }
        public IMessageEncoder Encoder { get; set; }
        public IMessageDecoder Decoder { get; set; }
        public IByteBufAllocator Allocator { get; set; }
        /// <summary>
        /// 链接适配器
        /// </summary>
        public IConnection ConnectionAdapter { get; set; }
        public abstract bool IsActive { get; protected set; }
        public abstract bool IsParsing { get; protected set; }
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

        public int Backlog { get; set; }
        public INode LocalEndpoint { get;set; }
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
        protected virtual void ReceivedData(NetworkData availableData, RefactorResponseChannel responseChannel)
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
