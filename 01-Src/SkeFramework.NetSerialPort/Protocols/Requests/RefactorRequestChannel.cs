using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Configs.Enums;
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
        /// <summary>
        /// 通信基类
        /// </summary>
        private readonly ReactorBase _reactor;
        /// <summary>
        /// 协议发送监听器
        /// </summary>
        public SenderListenser Sender;
        /// <summary>
        /// 链接配置
        /// </summary>
        protected IConnectionConfig connectionConfig;

        //2.声明事件；   
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
        #region 构造函数
        protected RefactorRequestChannel(ReactorBase reactor, string controlCode)
            : this(reactor, reactor.LocalEndpoint, controlCode)
        {
        }
        protected RefactorRequestChannel(ReactorBase reactor, INode node,string controlCode)
        {
            _reactor = reactor;
            Decoder = _reactor.Decoder.Clone();
            Encoder = _reactor.Encoder.Clone();
            Allocator = _reactor.Allocator;
            Local = reactor.LocalEndpoint;
            RemoteHost = node == null ? reactor.LocalEndpoint.Clone() as INode : node;
            this.Created = DateTime.Now;
            Dead = false;
            ControlCode = controlCode;
            Timeout = NetworkConstants.BackoffIntervals[6];
            this.Sender = new SenderListenser(this);
        }
        #endregion

        public INode RemoteHost { get; set; }
        public INode Local { get; set; }
        public IMessageEncoder Encoder { get; set; }
        public IMessageDecoder Decoder { get; set; }
        public IByteBufAllocator Allocator { get; set; }

        #region 请求状态
        public DateTime Created { get; private set; }
        public TimeSpan Timeout
        {
            get; set;
        }
        public bool Dead
        {
            get { return DateTime.Now.Subtract(this.Created.Add(this.Timeout)).Ticks > 0; }
            set
            {
                if (value)
                {
                    this.Timeout = TimeSpan.FromSeconds(0);
                    this.Sender.EndSend();
                }
                else this.Created = DateTime.Now;
            }
        }

        public bool WasDisposed { get; private set; }

        public bool Receiving { get; set; }

        public bool IsOpen()
        {
            return _reactor.IsActive;
        }

        /// <summary>
        /// 响应控制命令码
        /// </summary>
        public string ControlCode { get; set; }
        #endregion

        public int MessagesInSendQueue
        {
            get { return 0; }
        }
        /// <summary>
        /// 是否是反应处理
        /// </summary>
        public bool ProcessModeWithResponse
        {
            get
            {
                if (this.connectionConfig == null)
                {
                    return true;
                }
                if (this.connectionConfig.HasOption(OptionKeyEnums.ProcessMode.ToString()))
                {
                    if (this.connectionConfig.GetOption(OptionKeyEnums.ProcessMode.ToString()).Equals(ProcessModeValue.Request.ToString()))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public abstract void Configure(IConnectionConfig config);

        public void Open()
        {

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
            //_reactor.Send(buffer, index, length, destination);
            NetworkData data = NetworkData.Create(destination, buffer, length);
            this.Send(data);
        }
      
        /// <summary>
        /// 接受消息触发
        /// </summary>
        /// <param name="data"></param>
        public void InvokeReceiveIfNotNull(NetworkData data)
        {
            OnReceive(data);
        }
        /// <summary>
        /// 方法被实现直接调用，以将数据发送给它
        ///     <see cref="IConnection" />.
        ///     Can also be called by the socket itself if this reactor doesn't use <see cref="ReactorProxyResponseChannel" />.
        /// </summary>
        /// <param name="data">The data to pass directly to the recipient</param>
        public virtual void OnReceive(NetworkData data)
        {
            BeginReceive();
            if (this.ReceiveList != null)
            {
                this.ReceiveList(data,this);   //发出警报
            }
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
        /// <summary>
        /// Case发送帧 </summary>
        /// <param name="frame">    发送帧 </param>
        /// <param name="interval"> 发送间隔 </param>
        /// <param name="sendTimes"> 发送次数 </param>
        protected virtual void CaseSendFrame(NetworkData frame, int interval, int sendTimes)
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
