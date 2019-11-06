using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Listenser;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Requests
{
    /// <summary>
    /// 协议请求处理
    /// </summary>
   public abstract class RefactorRequestChannel : IConnection
    {
        private readonly ReactorBase _reactor;
        /// <summary>
        /// 协议发送监听器
        /// </summary>
        public SenderListenser Sender;
        //protected ICircularBuffer<NetworkData> UnreadMessages = new ConcurrentCircularBuffer<NetworkData>(1000);

        protected RefactorRequestChannel(ReactorBase reactor)
            : this(reactor,null)
        {
        }

        protected RefactorRequestChannel(ReactorBase reactor, INode node)
        {
            _reactor = reactor;
            Decoder = _reactor.Decoder.Clone();
            Encoder = _reactor.Encoder.Clone();
            Allocator = _reactor.Allocator;
            Local = reactor.LocalEndpoint;
            RemoteHost = node;
            this.Created = DateTime.Now;
            Dead = false;
            Timeout = NetworkConstants.BackoffIntervals[6];
            this.Sender = new SenderListenser(this);

        }



        public event ReceivedDataCallback Receive
        {
            add { }
            // ReSharper disable once ValueParameterNotUsed
            remove { }
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

        public void Close()
        {
            _reactor.CloseConnection(this);
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

        public void InvokeReceiveIfNotNull(NetworkData data)
        {
            OnReceive(data);
        }

        public virtual void Send(NetworkData data)
        {
            _reactor.Send(data);
        }

        public void Send(byte[] buffer, int index, int length, INode destination)
        {
            if (destination == null)
            {
                destination = this._reactor.LocalEndpoint;
            }
            _reactor.Send(buffer, index, length, destination);
        }
        /// <summary>
        /// Case发送帧 </summary>
        /// <param name="frame">    发送帧 </param>
        /// <param name="interval"> 发送间隔 </param>
        /// <param name="sendTimes"> 发送次数 </param>
        protected  virtual void CaseSendFrame(NetworkData frame, int interval, int sendTimes)
        {
            if (null == frame)
            {
                return;
            }
            this.Sender.FrameBeSent = frame;
            this.Sender.Interval = interval;
            this.Sender.TotalSendTimes = sendTimes;
            this.Sender.BeginSend();
        }
        /// <summary>
        /// 开始发送
        /// </summary>
        /// <param name="connectionTask"></param>
        public abstract void ExecuteTaskSync(ConnectionTask connectionTask);

        /// <summary>
        /// 开始接受
        /// </summary>
        public abstract void BeginReceiveInternal();

        /// <summary>
        ///     Method is called directly by the <see cref="ReactorBase" /> implementation to send data to this
        ///     <see cref="IConnection" />.
        ///     Can also be called by the socket itself if this reactor doesn't use <see cref="ReactorProxyResponseChannel" />.
        /// </summary>
        /// <param name="data">The data to pass directly to the recipient</param>
        internal virtual void OnReceive(NetworkData data)
        {

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

        public void StopReceive()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
