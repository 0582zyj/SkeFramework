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
using SkeFramework.NetSerialPort.Protocols.Connections.Tasks;
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
            : this(reactor, request, request.RemoteHost)
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
            if (node != null)
            {
                RemoteHost = node;
            }
            else
            {
                RemoteHost = reactor.LocalEndpoint.Clone() as INode;
            }
            this.Timeout = NetworkConstants.BackoffIntervals[6];
            this.Created = DateTime.Now;
            this.Dead = false;
            this.WasDisposed = true;
        }



        #region 声明事件 
        protected event ReceivedDataCallback ReceiveList;

        public event ReceivedDataCallback Receive
        {
            add { ReceiveList += value; }
            // ReSharper disable once ValueParameterNotUsed
            remove { ReceiveList -= value; }
        }
        protected event SendDataCallback SendList;
        public event SendDataCallback SendCallback
        {
            add { SendList += value; }
            // ReSharper disable once ValueParameterNotUsed
            remove { SendList -= value; }
        }
        #endregion

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

        public bool Receiving { get; set; }

        public bool IsOpen()
        {
            return _reactor.IsActive;
        }
        /// <summary>
        /// 响应控制命令码
        /// </summary>
        public string ControlCode { get ; set; }

        public int MessagesInSendQueue
        {
            get { return 0; }
        }
        /// <summary>
        /// 链接状态
        /// </summary>
        public ResultStatusCode connectionStatus { get; set; }


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
            BeginReceiveInternal();
        }

        public void StopReceive()
        {
            if (requestChannel != null)
            {
                this.requestChannel.StopReceive();
            }
            StopReceiveInternal();
        }

        public void Close()
        {
            _reactor.CloseConnection(this);
        }

        public virtual void Send(NetworkData data)
        {
            _reactor.Send(data);
            if (SendList != null)
            {
                SendList(data, this);
            }
        }

        public void Send(byte[] buffer, int index, int length, INode destination)
        {
            if (destination == null)
            {
                destination = this._reactor.LocalEndpoint;
            }
            NetworkData data = NetworkData.Create(destination, buffer, length);
            this.Send(data);
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
            if (this.ReceiveList != null)
            {
                this.ReceiveList(data, this);   //发出警报
            }
        }


        protected abstract void StopReceiveInternal();

        public void InvokeReceiveIfNotNull(NetworkData data)
        {

            StopReceive();
            OnReceive(data);
        }

       
        #region IDisposable members

        public virtual void Dispose()
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
