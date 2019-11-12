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
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Response
{
    /// <summary>
    /// 远程实例响应
    /// </summary>
    public abstract class RefactorResponseChannel : IConnection
    {
        private readonly ReactorBase _reactor;
        internal RefactorRequestChannel requestChannel;

        //protected ICircularBuffer<NetworkData> UnreadMessages = new ConcurrentCircularBuffer<NetworkData>(1000);

        protected RefactorResponseChannel(ReactorBase reactor, RefactorRequestChannel request)
            : this(reactor, request, null)
        {
        }

        protected RefactorResponseChannel(ReactorBase reactor, RefactorRequestChannel request, INode node)
        {
            _reactor = reactor;
            requestChannel = request;
            Decoder = _reactor.Decoder.Clone();
            Encoder = _reactor.Encoder.Clone();
            Allocator = _reactor.Allocator;
            Local = reactor.LocalEndpoint;
            RemoteHost = node;
            Timeout = NetworkConstants.BackoffIntervals[6];
            this.Created = DateTime.Now;
            Dead = false;
        }



        public event ReceivedDataCallback Receive
        {
            add { }
            // ReSharper disable once ValueParameterNotUsed
            remove {  }
        }


        public IMessageEncoder Encoder { get; set; }
        public IMessageDecoder Decoder { get; set; }
        public IByteBufAllocator Allocator { get; set; }

        public DateTime Created { get; private set; }
        public INode RemoteHost { get; set; }
        public INode Local { get; set; }

        public TimeSpan Timeout
        {
            get;set;
        }

        public bool Dead { get { return DateTime.Now.Subtract(this.Created.Add(this.Timeout)).Ticks > 0; } set { if (value) this.Timeout = TimeSpan.FromSeconds(0); else this.Created = DateTime.Now; } }


        public bool WasDisposed { get; private set; }

        public bool Receiving
        {
            get { return _reactor.IsActive; }
        }

        public bool IsOpen()
        {
            return _reactor.IsActive;
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
        ///     Can also be called by the socket itself if this reactor doesn't use <see cref="RefactorProxyResponseChannel" />.
        /// </summary>
        /// <param name="data">The data to pass directly to the recipient</param>
        public virtual void OnReceive(NetworkData data)
        {
            requestChannel.Dead = false;
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
                }
            }
        }

        #endregion
    }
}
