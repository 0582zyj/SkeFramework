using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Constants;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections.Tasks;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.DataFrame;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Connections
{
    /// <summary>
    ///  链接适配
    /// </summary>
    public class ReactorConnectionAdapter : IConnection
    {
        #region 事件
        public event ReceivedDataCallback Receive
        {
            add { _reactor.OnReceive += value; }
            remove { _reactor.OnReceive -= value; }
        }

        public event SendDataCallback SendCallback
        {
            add { _reactor.OnSend += value; }
            remove { _reactor.OnSend -= value; }
        }
        #endregion

        /// <summary>
        /// 通信
        /// </summary>
        protected ReactorBase _reactor;
    
        public ReactorConnectionAdapter(ReactorBase reactor)
        {
            _reactor = reactor;
            connectionDocker = new ConnectionDocker();
            taskDocker = new TaskManager(this);
            networkDataDocker = new NetworkDataDocker();
            RemoteHost = reactor.LocalEndpoint; 
            this.protocolEvents = new WaitHandle[3];
            this.protocolEvents[(int)ProtocolEvents.ProtocolExit] = new ManualResetEvent(false);
            this.protocolEvents[(int)ProtocolEvents.PortReceivedData] = new AutoResetEvent(false);
            this.protocolEvents[(int)ProtocolEvents.TaskArrived] = new AutoResetEvent(false);
        }

        /// <summary>
        /// 编码器
        /// </summary>
        public IMessageEncoder Encoder
        {
            get { return _reactor.Encoder; }
        }
        /// <summary>
        /// 解码器
        /// </summary>
        public IMessageDecoder Decoder
        {
            get { return _reactor.Decoder; }
        }
        /// <summary>
        /// 缓冲区
        /// </summary>
        public IByteBufAllocator Allocator
        {
            get { return _reactor.Allocator; }
        }
        /// <summary>
        /// 创建事件
        /// </summary>
        public DateTime Created { get; private set; }
        /// <summary>
        /// 远端节点
        /// </summary>
        public INode RemoteHost { get; private set; }
        /// <summary>
        /// 本地节点
        /// </summary>
        public INode Local
        {
            get { return _reactor.LocalEndpoint; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Timeout { get; private set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        public bool Dead { get { return DateTime.Now.Subtract(this.Created.Add(this.Timeout)).Ticks > 0; } set { } }
        /// <summary>
        /// 是否释放
        /// </summary>
        public bool WasDisposed { get;  set; }
        /// <summary>
        /// 是否接收中
        /// </summary>
        public bool Receiving { get; set; }
        /// <summary>
        /// 是否打开
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
        /// 正在发送数量
        /// </summary>
        public int MessagesInSendQueue
        {
            get { return 0; }
        }
        #region Initialize
        /// <summary>
        /// 启动配置
        /// </summary>
        /// <param name="config"></param>
        public void Configure(IConnectionConfig config)
        {
            _reactor.Configure(config);
        }
        #endregion

        #region 开启关闭
        /// <summary>
        /// 启动协议进程
        /// </summary>
        public void Open()
        {
            try
            {
                //receivingFrame = null;
                // 1、打开新的通讯端口
                if (_reactor != null)
                {
                    if (!_reactor.IsActive)
                    {
                        _reactor.Start();
                        Thread.Sleep(1);
                    }
                }
                Console.WriteLine("启动协议线程");
                // 2、启动协议线程
                if ((_reactor != null && (_reactor.IsActive)))
                {
                    if (!ProtocolThreadIsAlive)
                    {
                        this.ProtocolThreadIsAlive = true;
                        this.connectionStatus = ResultStatusCode.CONNECTION_OPEN;
                        this.ThreadProtocol = new Thread(new ThreadStart(ProtocolThreadProcess));
                        ((ManualResetEvent)protocolEvents[(int)ProtocolEvents.ProtocolExit]).Reset();
                        this.ThreadProtocol.Name = "未命名规约";
                        this.ThreadProtocol.IsBackground = true;
                        this.ThreadProtocol.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动协议进程:" + ex.ToString());
            }
        }
        /// <summary>
        /// 关闭协议进程
        /// </summary>
        public void Close()
        {
            Console.WriteLine("关闭协议线程");
            // 1、关闭协议线程
            if (this.ThreadProtocol != null)
            {
                ((EventWaitHandle)protocolEvents[(int)ProtocolEvents.ProtocolExit]).Set();
                if (this.ThreadProtocol != null)
                {
                    if (!this.ThreadProtocol.Join(3000))
                    {
                        this.ThreadProtocol.Abort(); // 强行退出线程。
                        this.ThreadProtocol.Join();  // 若线程还未退出，可能是线程函数中catch到了Abort扔出的ThreadAbortException异常后，让线程继续运行所致。
                    }
                }
            }
        }
        #endregion

        #region 接受数据
        public void BeginReceive()
        {
            if (_reactor == null || !_reactor.IsActive)
                return;
            ///* 这个while及其判断目的是解决一次性接收多个帧数据时，及时将数据全部解析。
            // * （否则若收到两个帧，而没有此while，则只能解析前面那个帧，而后面的帧数据仍然在缓冲区，
            // * 要等待下一次接收到数据触发了接收事件才能获得解析的机会）*/
            int revTimes = this.networkDataDocker.BusinessCaseList.Count;
            int FailedFrameCount = 0; //蓝牙端口接收数据缓存可能有N条数据
            while ((FailedFrameCount < 1) && revTimes>0)
            {
                ProcessReceivedData(networkDataDocker.GetNetworkData());
                revTimes--;
                // 若远端无休止地发送数据，这边处理较慢时将导致while一直循环，在这种情况下Sleep将导致切换线程，而不至于一直占用CPU。
                Thread.Sleep(10);
                //因此在再此while中循环已经没有意义，等下面的数据来到后将会由事件触发。
                //if (receivingFrame != null)
                //    break;
            }
        }

        public void BeginReceive(ReceivedDataCallback callback)
        {
            Receive += callback;
        }

        public void StopReceive()
        {
            Receive -= (data, channel) => { };
        }
        #endregion

        #region 发送数据
        public void Send(NetworkData data)
        {
            this.Send(data.Buffer, 0, data.Length, data.RemoteHost);
        }

        public void Send(byte[] buffer, int index, int length)
        {
            this.Send(buffer, index, length, _reactor.LocalEndpoint);
        }

        public void Send(byte[] buffer, int index, int length, INode destination)
        {
            if (destination == null)
            {
                destination = this._reactor.LocalEndpoint;
            }
            _reactor.Send(buffer, index, length, destination);
        }
        #endregion

        #region 协议线程
        /// <summary>
        /// 协议运行的线程。
        /// </summary>
        private Thread ThreadProtocol = null;
        /// <summary>
        /// 线程是否运行
        /// </summary>
        private bool ProtocolThreadIsAlive = false;
        /// <summary>
        /// 线程轮询的时间间隔（默认值为100毫秒）（单位：毫秒）
        /// </summary>
        protected int pollingInterval = 100;
        /// <summary>
        /// 协议等待的事件。
        /// </summary>
        public readonly WaitHandle[] protocolEvents = null;
        /// <summary>
        /// 链接容器
        /// </summary>
        protected ConnectionDocker connectionDocker;
        /// <summary>
        /// 任务容器
        /// </summary>
        protected TaskManager taskDocker;
        /// <summary>
        /// 数据处理缓冲区
        /// </summary>
        public NetworkDataDocker networkDataDocker;
        #endregion

        #region 协议线程的处理过程
        /// <summary>
        /// 协议运行的线程执行体。
        /// </summary>
        void ProtocolThreadProcess()
        {
            Initialize();
            do
            {
                try
                {
                    // 1、协议的轮询处理
                    Polling();
                    // 2、协议线程的事件处理
                    if (null != protocolEvents)
                    {
                        int proEvent = WaitHandle.WaitAny(protocolEvents, pollingInterval, false);
                        switch (proEvent)
                        {
                            case (int)ProtocolEvents.ProtocolExit:
                                this.ProtocolThreadIsAlive = false;
                                this.connectionStatus = ResultStatusCode.CONNECTION_CLOSE;
                                break;
                            case (int)ProtocolEvents.TaskArrived:
                                ProcessNewTask();
                                break;
                            case (int)ProtocolEvents.PortReceivedData:
                                BeginReceive();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            } while (this.ProtocolThreadIsAlive);
            UnInitialize();
        }
        /// <summary>
        /// 轮询处理
        /// </summary>
        private void Polling()
        {
            ProcessNewTask();
            ProcessAsyncTaskOvertime();
            connectionDocker.ProcessCase();
            connectionDocker.ProcessCaseOvertime();
            OnPolling();
        }
        /// <summary>
        /// 处理新任务
        /// </summary>
        private void ProcessNewTask()
        {
            try
            {
                var newTaskList = this.taskDocker.TaskList.Where(o => o.TaskState == TaskState.NewTask).ToList();
                foreach(var task in newTaskList)
                {
                    if (task.TaskState == TaskState.NewTask)
                    {
                        string log = String.Format("{0}:处理新任务:{1}", DateTime.Now.ToString("hh:mm:ss"), task.Name);
                        Console.WriteLine(log);
                        ProcessTask(task);
                        if (!task.Dead)
                        {
                            this.connectionDocker.AddCase(task.GetRelatedProtocol());
                        }
                        else
                        {
                            task.Complete();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                var m = exception.Message;
                Console.WriteLine("处理新任务:" + m.ToString());
            }
        }
        /// <summary>
        /// 判断异步任务是否超时并处理。
        /// </summary>
        private void ProcessAsyncTaskOvertime()
        {
            try
            {
                lock (this.taskDocker.TaskList)
                {
                    var taskList = this.taskDocker.TaskList;
                    for (int i = 0; i < taskList.Count; ++i)
                    {
                        if (taskList[i].AsyncTaskOvertime || taskList[i].Dead)
                        {
                            taskList[i].Complete(TaskState.Completed);
                            this.taskDocker.RemoveTask(taskList[i]);
                            this.connectionDocker.SetCaseAsDead(taskList[i]);
                            string log = String.Format("{0}:处理超时任务:{1}", DateTime.Now.ToString("hh:mm:ss"), taskList[i].Name);
                            Console.WriteLine(log);
                            //break;
                        }
                        else
                        {
                            if (taskList[i].GetRelatedProtocol().Dead)
                            {
                                taskList[i].Complete(TaskState.Completed);
                            }
                            else
                            {
                                this.taskDocker.SetTaskTimeout(taskList[i].GetRelatedProtocol());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("ProcessAsyncTaskOvertime：{0}", ex.ToString());
            }
        }
        /// <summary>
        /// 协议的初始化工作可以重载此函数来实现。
        /// 协议有一个自己的运作协议线程，当该线程开始运行时会调用此函数。
        /// （因此如果您需要在协议中创建自己的线程或窗体等，可以重载此函数来实现）。
        /// 通过重载UnInitialize()来释放资源。基类中此函数未做任何工作。
        /// </summary>
        protected virtual void Initialize() {  }
        /// <summary>
        /// 协议线程退出时的工作可以重载此函数来实现。
        /// 注：协议有一个自己的运作协议线程，当该线程在退出前会调用此函数。基类中此函数未做任何工作。
        /// </summary>
        protected virtual void UnInitialize() { }
        /// <summary>
        /// 协议线程轮询调用的函数。
        /// 线程每隔一段时间将调用此函数，时间间隔可以通过pollingInterval设置，该时间间隔默认值为100毫秒。
        /// </summary>
        protected virtual void OnPolling() { }
        /// <summary>
        /// 协议一收到任务，此函数将被调用。
        /// </summary>
        /// <param name="newTask">收到的任务。</param>
        protected virtual void ProcessTask(ConnectionTask connectionTask) { }      
        #endregion

        #region 协议任务处理过程
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public virtual bool ExecuteTaskSync(ConnectionTask task, int timeout)
        {
            //if (!this.ProtocolIsRunning)
            //    return false;
            task.InitSynchObject();
            this.taskDocker.AddTask(task);
            task.WaitBeCompleted(timeout);
            if (task.TaskState != TaskState.Completed)
            {
                task.SetAsOvertime();
                if (task.Result == null)
                {
                    task.Result = new TaskResult(false, "", new object());
                }
                this.taskDocker.RemoveTask(task);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 异步执行任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        public virtual void ExecuteTaskAsync(ConnectionTask task, AsyncCallback asyncCallback, int timeout)
        {
            if (this.ProtocolThreadIsAlive)
            {
                task.AsyncInvokeSetup(asyncCallback, task, timeout);
                AddTask(task);
            }
            else
            {
                if (asyncCallback != null)
                {
                    task.AsyncInvokeSetup(asyncCallback, task, timeout);
                    task.Complete(TaskState.NewTask);
                }
            }
        }
        /// <summary>
        /// 给规约增加任务
        /// </summary>
        /// <param name="task">要添加的任务。</param>
        public virtual void AddTask(ConnectionTask task)
        {
            if (task == null)
                return;
            lock (this.taskDocker)
            {
                if (!this.taskDocker.TaskList.Contains(task))
                {
                    this.taskDocker.AddTask(task);
                    ((EventWaitHandle)protocolEvents[(int)ProtocolEvents.TaskArrived]).Set();
                }
            }
        }
        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="connectionTask"></param>
        /// <returns></returns>
        public virtual bool RemoveTask(ConnectionTask connectionTask)
        {
           return this.taskDocker.RemoveTask(connectionTask);
        }
        #endregion

        #region 请求和响应
        /// <summary>
        /// 获取最新未收到消息的请求
        /// </summary>
        /// <returns></returns>
        public virtual IConnection GetConnection(FrameBase frame)
        {
            IList<IConnection> connections = this.connectionDocker.BusinessCaseList.
                Where(o => o.Receiving).OrderByDescending(o => o.Created).ToList();
            return connections.LastOrDefault();
        }
        /// <summary>
        /// 原始数据解析处理
        /// </summary>
        /// <param name="OriginalBuffer"></param>
        /// <returns></returns>
        public virtual FrameBase ParsingReceivedData(byte[] OriginalBuffer)
        {
            FrameBase frameBase = new FrameBase(OriginalBuffer,null);
            //触发整条记录的处理
            return frameBase;
        }
        /// <summary>
        /// 处理端口缓冲区收到的端口数据。
        /// </summary>
        protected virtual void ProcessReceivedData(NetworkData networkData)
        {
            IConnection connection = this.connectionDocker.GetCase(networkData.RemoteHost.TaskTag);
            if (connection != null && connection is RefactorRequestChannel)
            {
                string content = connection.Encoder.ByteEncode(networkData.Buffer);
                string log = String.Format("{0}:协议层消息处理【{1}】：{2}",
                    DateTime.Now.ToString("hh:mm:ss"), networkData.RemoteHost.ToString(), content);
                Console.WriteLine(log);
                RefactorRequestChannel requestChannel = (RefactorRequestChannel)connection;
                requestChannel.StopReceive();
            }
        }
        #endregion

        #region IDisposable methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!WasDisposed)
            {
                if (disposing)
                {
                    Close();
                    if (_reactor != null)
                    {
                        ((IDisposable)_reactor).Dispose();
                        _reactor = null;
                    }
                }
            }
            WasDisposed = true;
        }

        #endregion
    }
    /// <summary>
    /// 协议线程阻塞而等待要处理的事件。
    /// </summary>
    internal enum ProtocolEvents : int
    {
        /// <summary>
        /// 退出协议。
        /// </summary>
        ProtocolExit = 0,
        /// <summary>
        /// 通讯端口收到数据。
        /// </summary>
        PortReceivedData,
        /// <summary>
        /// 收到应用发送的任务。
        /// </summary>
        TaskArrived,
    }
}
