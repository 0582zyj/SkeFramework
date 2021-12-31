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
using SkeFramework.NetSerialPort.Protocols.Connections.Tasks;
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
        /// 声明接收事件
        /// </summary>
        protected event ReceivedDataCallback ReceiveList;
        public event ReceivedDataCallback Receive
        {
            add { ReceiveList += value; }
            // ReSharper disable once ValueParameterNotUsed
            remove { ReceiveList -= value; }
        }
        /// <summary>
        /// 声明发送事件
        /// </summary>
        protected event SendDataCallback SendList;
        public event SendDataCallback SendCallback
        {
            add { SendList += value; }
            // ReSharper disable once ValueParameterNotUsed
            remove { SendList -= value; }
        }

        /// <summary>
        /// 通信基类
        /// </summary>
        protected readonly ReactorBase _reactor;
        /// <summary>
        /// 协议发送监听器
        /// </summary>
        public SenderListenser Sender;
        /// <summary>
        /// 链接配置
        /// </summary>
        protected IConnectionConfig connectionConfig;
        /// <summary>
        /// 上次活跃时间
        /// </summary>
        protected DateTime LastActiveTime;
        #region 构造函数
        protected RefactorRequestChannel(ReactorBase reactor, string controlCode)
            : this(reactor, reactor.Local, controlCode)
        {
        }
        protected RefactorRequestChannel(ReactorBase reactor, INode node,string controlCode)
        {
            _reactor = reactor;
            Decoder = _reactor.Decoder.Clone();
            Encoder = _reactor.Encoder.Clone();
            Allocator = _reactor.Allocator;
            Local = reactor.Local;
            RemoteHost = node == null ? reactor.Local.Clone() as INode : node;
            this.Created = DateTime.Now;
            this.LastActiveTime = DateTime.Now;
            Dead = false;
            ControlCode = controlCode;
            Timeout = NetworkConstants.BackoffIntervals[3];
            this.Sender = new SenderListenser(this);
            Reset = false;
        }
        #endregion
      
        /// <summary>
        /// 远端节点
        /// </summary>
        public INode RemoteHost { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public INode Local { get; set; }
        /// <summary>
        /// 消息编码器
        /// </summary>
        public IMessageEncoder Encoder { get; set; }
        /// <summary>
        /// 消息解码器
        /// </summary>
        public IMessageDecoder Decoder { get; set; }
        /// <summary>
        /// 字节处理器
        /// </summary>
        public IByteBufAllocator Allocator { get; set; }

        #region 请求状态
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime Created { get; private set; }
        /// <summary>
        /// 超时时间设置
        /// </summary>
        public TimeSpan Timeout
        {
            get; set;
        }
        /// <summary>
        /// 是否已过期
        /// </summary>
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
        /// <summary>
        /// 任务结束是否立即关闭协议
        /// </summary>
        public bool WasDisposed { get { return DateTime.Now.Subtract(this.LastActiveTime.Add(this.Timeout)).Ticks > 0; } }
        /// <summary>
        /// 当前是否正在接受
        /// </summary>
        public bool Receiving { get; set; }
        /// <summary>
        /// 协议是否已打开
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            return _reactor.IsActive;
        }
        /// <summary>
        /// 响应控制命令码
        /// </summary>
        public string ControlCode { get; set; }
        /// <summary>
        /// 链接状态
        /// </summary>
        public ResultStatusCode connectionStatus { get; set; }
        /// <summary>
        /// 重置发送监听器
        /// </summary>
        public bool Reset { get; set; }
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
        
        #region 开启和关闭
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="config"></param>
        public abstract void Configure(IConnectionConfig config);
        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            this.connectionStatus = ResultStatusCode.CONNECTION_OPEN;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            _reactor.CloseConnection(this);
            this.connectionStatus = ResultStatusCode.CONNECTION_CLOSE;
        }
        #endregion
        
        #region 发送数据
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
            BeginReceive();
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="destination"></param>
        public void Send(byte[] buffer, int index, int length, INode destination)
        {
            if (destination == null)
            {
                destination = this._reactor.Local;
            }
            NetworkData data = NetworkData.Create(destination, buffer, length);
            this.Send(data);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        public virtual void Send(NetworkData data)
        {
            _reactor.Send(data);
            if (SendList != null)
            {
                SendList(data, this);
            }
        }
        #endregion

        #region 接受数据
        /// <summary>
        /// 接受消息触发
        /// </summary>
        /// <param name="data"></param>
        public void InvokeReceiveIfNotNull(NetworkData data)
        {
            StopReceive();
            OnReceive(data);
        }
        /// <summary>
        /// 方法被实现直接调用，以将数据发送给它
        /// </summary>
        /// <param name="data">The data to pass directly to the recipient</param>
        public virtual void OnReceive(NetworkData data)
        {
            if (this.ReceiveList != null)
            {
                this.ReceiveList(data,this);   //发出警报
            }
        }
        #endregion

        #region 接收/超时
        public void BeginReceive()
        {
            this.Receiving = true;
            this.LastActiveTime = DateTime.Now;
            this.BeginReceiveInternal();
        }
       
        public void BeginReceive(ReceivedDataCallback callback)
        {
            Receive += callback;
            BeginReceiveInternal();
        }
        public void StopReceive()
        {
            if (this.Sender.TotalSendTimes != NetworkConstants.WAIT_FOR_COMPLETE)
            {
                this.Receiving = false;
                this.Sender.EndSend();
            }
            StopReceiveInternal();
        }
        #endregion

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="connectionTask"></param>
        public abstract void ExecuteTaskSync(ConnectionTask connectionTask);
        /// <summary>
        /// 开始接受
        /// </summary>
        public abstract void BeginReceiveInternal();
        /// <summary>
        /// 结束接受
        /// </summary>
        public abstract void StopReceiveInternal();

        #region IDisposable members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }
        #endregion
    }
}
