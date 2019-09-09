using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Response
{
    /// <summary>
    /// 远程实例连接
    /// </summary>
    public abstract class ReactorResponseChannel : IConnection
    {
        private readonly ReactorBase _reactor;
        internal Socket Socket;

        //protected ICircularBuffer<NetworkData> UnreadMessages = new ConcurrentCircularBuffer<NetworkData>(1000);

        protected ReactorResponseChannel(ReactorBase reactor, Socket outboundSocket )
            : this(reactor, outboundSocket, (IPEndPoint)outboundSocket.RemoteEndPoint)
        {
        }

        protected ReactorResponseChannel(ReactorBase reactor, Socket outboundSocket, IPEndPoint endPoint)
        {
            _reactor = reactor;
            Socket = outboundSocket;
            Decoder = _reactor.Decoder.Clone();
            Encoder = _reactor.Encoder.Clone();
            Allocator = _reactor.Allocator;
            Local = reactor.LocalEndpoint.ToNode(reactor.Transport);
            RemoteHost = NodeBuilder.FromEndpoint(endPoint);
        }



        public event ReceivedDataCallback Receive
        {
            add { }
            // ReSharper disable once ValueParameterNotUsed
            remove {  }
        }

       
        public IMessageEncoder Encoder { get; }
        public IMessageDecoder Decoder { get; }
        public IByteBufAllocator Allocator { get; }

        public DateTimeOffset Created { get; private set; }
        public INode RemoteHost { get; }
        public INode Local { get; }

        public TimeSpan Timeout
        {
            get { return TimeSpan.FromSeconds(Socket.ReceiveTimeout); }
        }

        public TransportType Transport
        {
            get
            {
                if (Socket.ProtocolType == ProtocolType.Tcp)
                {
                    return TransportType.Tcp;
                }
                return TransportType.Udp;
            }
        }

        public bool Blocking
        {
            get { return Socket.Blocking; }
            set { Socket.Blocking = value; }
        }

        public bool WasDisposed { get; private set; }

        public bool Receiving
        {
            get { return _reactor.IsActive; }
        }

        public bool IsOpen()
        {
            return Socket != null && Socket.Connected;
        }

        public int Available
        {
            get { return Socket == null ? 0 : Socket.Available; }
        }

        public int MessagesInSendQueue
        {
            get { return 0; }
        }


        public abstract void Configure(IConnectionConfig config);

        public void Open()
        {
           
        }

        public void BeginReceive()
        {
            BeginReceiveInternal();
        }

        public void BeginReceive(ReceivedDataCallback callback)
        {
            Receive += callback;
            //foreach (var msg in UnreadMessages.DequeueAll())
            //{
            //    NetworkEventLoop.Receive(msg, this);
            //}

            BeginReceiveInternal();
        }

        public void StopReceive()
        {
            StopReceiveInternal();
        }

        public void Close()
        {
            _reactor.CloseConnection(this);
        }

        public virtual void Send(NetworkData data)
        {
            _reactor.Send(data);
        }

        public void Send(byte[] buffer, int index, int length, INode destination)
        {
            _reactor.Send(buffer, index, length, destination);
        }

        protected abstract void BeginReceiveInternal();


        /// <summary>
        ///     Method is called directly by the <see cref="ReactorBase" /> implementation to send data to this
        ///     <see cref="IConnection" />.
        ///     Can also be called by the socket itself if this reactor doesn't use <see cref="ReactorProxyResponseChannel" />.
        /// </summary>
        /// <param name="data">The data to pass directly to the recipient</param>
        internal virtual void OnReceive(NetworkData data)
        {
          
        }


        protected abstract void StopReceiveInternal();

        public void InvokeReceiveIfNotNull(NetworkData data)
        {
            OnReceive(data);
        }

       
        #region IDisposable members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!WasDisposed)
            {
                WasDisposed = true;
                if (disposing)
                {
                    Close();
                    Socket = null;
                }
            }
        }

        #endregion
    }
}
