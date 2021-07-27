# SkeFramework.NetSerialPort

#### 介绍
SkeFramework.NetSerialPort是创客快速开发框架里关于串口通信实现的通信库，可通过该库快速完成串口类的通信开发。

#### 核心实现流程

![](https://p3-tt.byteimg.com/origin/pgc-image/33fc4b9c3067438a9167322003c28893?from=pc)


#### 安装教程

1.  Package Manager=>Install-Package SkeFramework.NetSerialPort -Version 1.0.2.1

#### 使用说明

1.  首先先引入SkeFramework.NetSerialPort库，目前该库暂未发布到Nuget包管理器，可通过联系本人或者下载源码编译生成，版本号目前是V1.02.01.
2.  实现ReactorConnectionAdapter的ParsingReceivedData【原始数据解析】GetConnection【返回数据链接分配】 ProcessReceivedData【协议数据处理】ProcessTask【新任务处理】
3.  创建规约协议代理类【ProtocolProxyAgent】-利用引导程序类ServerBootstrap快速对串口及其工作线程的开启和关闭，除此还可以通过DefaultChannelPromise实现对请求监听回调的增、删和数据监听的回调处理。具体规约实现【BusinessCase】，通过继承默认请求类【RefactorProxyRequestChannel】，根据实际需求实现配置【Configure】、执行发送任务【ExecuteTaskSync】、数据组帧【CreateNetworkData】、接收处理【OnReceive】、超时处理【StopReceiveInternal】
4.  数据帧类【DataFrame】，根据通信规约格式和通信基类【FrameBase】实现数据帧类的封装，可根据具体业务抽象对外接口层和具体协议层的数据交互实体。
5.  实现ReactorConnectionAdapter的ParsingReceivedData【原始数据解析】GetConnection【返回数据链接分配】 ProcessReceivedData【协议数据处理】ProcessTask【新任务处理】
6.  对外接口层【DataHandle】，根据实际业务继承基础接口【IDataHandleBase】和基础实现类【DataHandleBase】，创建一个新任务，通过ProtocolProxyAgent新增任务处理方式达到请求具体协议的通信。

#### 基本使用例子

![](https://p6-tt.byteimg.com/origin/pgc-image/3b855f708ab94f4f86f85056461b9231?from=pc)

ProtocolUT

```
public sealed class ProtocolUT : ReactorConnectionAdapter
    {
        /// <summary>
        /// 规约基类，具体实现根据工厂方法实例化
        /// </summary>
        private IConnection commCase_Send = null;
      
      
        public ProtocolUT(string ProtocolName, ReactorBase reactor)
            : base(reactor)
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialize()
        {
         
        }
        /// <summary>
        /// 协议一收到任务，此函数将被调用。
        /// </summary>
        /// <param name="newTask"></param>
        protected override void ProcessTask(ConnectionTask task)
        {
            task.SetAsBeProccessing();
            try
            {
                string cmdByte =task.Name;
                commCase_Send = this.connectionDocker.GetCase(cmdByte) as IConnection;
                if (commCase_Send == null || !(commCase_Send is RefactorRequestChannel))
                {
                    int taskname = Convert.ToInt32(task.Name);
                    switch (taskname)//应答
                    {
                        case ProtocolConst.APP_BROADCAST_SEARCH_NEW_HOST:
                            commCase_Send = new BroadcastSearchRequest(this._reactor);
                            break;
                        default:
                            string msg = String.Format("协议未实现。 TaskName:{0}; ", task.Name);
                            Console.WriteLine(msg);
                            task.Dead = true;
                            return;
                    }
                }
                else
                {
                    //commCase_Send.Task.Param = task.Param;
                    //commCase_Send.Task.Complete(TaskState.Processing);
                }
                if (commCase_Send != null)
                {
                    task.SetRelatedProtocol(commCase_Send);
                    ((RefactorRequestChannel)commCase_Send).ExecuteTaskSync(task);
                }
            }
            catch (Exception ex)
            {
                string msg = String.Format("协议收到任务，此函数被调用。 TaskName:{0}; Message:{1}", task.Name, ex.Message);
                Console.WriteLine(msg);
                task.Dead = true;
            }
        }
        /// <summary>
        /// 原始数据解析
        /// </summary>
        /// <param name="OriginalBuffer"></param>
        /// <returns></returns>
        public override FrameBase ParsingReceivedData(byte[] OriginalBuffer)
        {
            Console.WriteLine("串口收到数据<<--" + this.Encoder.ByteEncode(OriginalBuffer));
            FrameBase frame = new FrameBase(OriginalBuffer,null);
            FrameBase.ResultOfParsingFrame result = frame.ParseToFrame(OriginalBuffer);
            if (result.Equals(FrameBase.ResultOfParsingFrame.ReceivingCompleted))
            {
                int len = (int)OriginalBuffer[2];
                return frame;
            }
            return null;
        }
        /// <summary>
        /// 分配链接
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public override IConnection GetConnection(FrameBase frame)
        {
            return base.GetConnection(frame);
        }
    }
```
ProtocolProxyAgent

```
/// <summary>
    /// 协议代理商
    /// </summary>
    public class ProtocolProxyAgent
    {
        #region 单例模式
        /// <summary>
        /// 协议管理器
        /// </summary>
        private static ProtocolProxyAgent mSingleInstance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static ProtocolProxyAgent Instance()
        {
            if (null == mSingleInstance)
            {
                mSingleInstance = new ProtocolProxyAgent();
            }
            return mSingleInstance;
        }
        #endregion


        /// <summary>
        /// 通信基类
        /// </summary>
        private IReactor reactor;
        /// <summary>
        /// 协议适配类
        /// </summary>
        private IConnection connectionAdapter;
        /// <summary>
        /// 协议适配类
        /// </summary>
        public ReactorConnectionAdapter reactorConnectionAdapter
        {
            get
            {
                if (connectionAdapter is ReactorConnectionAdapter)
                {
                    return (ReactorConnectionAdapter)connectionAdapter;
                }
                return null;
            }
        }
        /// <summary>
        /// 协议监听容器
        /// </summary>
        private IChannelPromise channelListenser = new DefaultChannelPromise();

        #region 协议处理代理
        /// <summary>
        /// 端口是否打开
        /// </summary>
        /// <returns></returns>
        public bool IsReactorOpen
        {
            get
            {
                if (this.reactor == null)
                {
                    return false;
                }
                return this.reactor.IsActive;
            }
            private set { }
        }
        /// <summary>
        /// 启动一个通信
        /// </summary>
        /// <param name="nodeConfig"></param>
        public bool StartReactor(NodeConfig nodeConfig)
        {
            try
            {
                var bootstrapper = new ServerBootstrap()
               .WorkerThreads(2)
               //.SetConfig(new DefaultConnectionConfig().SetOption(OptionKeyEnums.ParseTimeOut.ToString(), 5))
               .Build();
                reactor = bootstrapper.NewReactor(NodeBuilder.BuildNode().Host(nodeConfig));
                reactor.ConnectionAdapter = new ProtocolUT("SerialProtocol", (ReactorBase)reactor);
                reactor.Start();
                connectionAdapter = reactor.ConnectionAdapter;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            return false;
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void StopReactor()
        {
            try
            {
                if (this.IsReactorOpen)
                {
                    connectionAdapter.StopReceive();
                    reactor.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
        #endregion

        #region 界面消息监听处理
        public void DataPointListener_Send(NetworkData networkData, IConnection requestChannel)
        {
            string content = requestChannel.Encoder.ByteEncode(networkData.Buffer);
        }
        public void DataPointListener_Receive(NetworkData incomingData, IConnection responseChannel)
        {
            channelListenser.OnReceivedDataPoint(incomingData, responseChannel.RemoteHost.TaskTag);
        }
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void AddDataPointListener(IChannelListener listener)
        {
            channelListenser.AddDataPointListener(listener);
        }
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveDataPointListener(IChannelListener listener)
        {
            channelListenser.RemoveDataPointListener(listener);
        }
        #endregion

    }
```
SingleRequestChannel

```
/// <summary>
    /// 单帧请求协议
    /// </summary>
    internal class SingleRequestChannel: RefactorProxyRequestChannel
    {
        /// <summary>
        /// 任务参数
        /// </summary>
        protected object TaskParm;

        public SingleRequestChannel(ReactorBase reactor,string controlCode)
        : this(reactor, null, controlCode)
        {
        }

        public SingleRequestChannel(ReactorBase reactor, INode endPoint, string controlCode)
       : base(reactor, endPoint, controlCode)
        {
            Receive += ProtocolProxyAgent.Instance().DataPointListener_Receive;
            SendCallback += ProtocolProxyAgent.Instance().DataPointListener_Send;
            this.Configure(ConstantConnConfig.ProcessModeRequest);
        }

        public override void ExecuteTaskSync(ConnectionTask connectionTask)
        {
            this.TaskParm = connectionTask != null ? connectionTask.Param : null;
            base.ExecuteTaskSync(connectionTask);
        }
        /// <summary>
        /// 创建发送帧
        /// </summary>
        /// <param name="bodyByte"></param>
        /// <returns></returns>
        protected FrameBase CreateSHSerialFrame(byte[] bodyByte)
        {
            byte cmd = Convert.ToByte(this.ControlCode);
            SHSerialFrame frame = new SHSerialFrame(cmd, bodyByte);
            frame.SetCheckBytes();
            return frame;
        }
        /// <summary>
        /// 创建发送帧
        /// </summary>
        /// <param name="bodyByte"></param>
        /// <returns></returns>
        protected FrameBase CreateSHSerialFrame(byte cmd,byte[] bodyByte)
        {
            SHSerialFrame frame = new SHSerialFrame(cmd, bodyByte);
            frame.SetCheckBytes();
            return frame;
        }
    }
	
```
IDataHandleBase和DataHandleBase

```
/// <summary>
    /// 基础协议请求接口
    /// </summary>
    public interface IDataHandleBase
    {
        /// <summary>
        /// 执行一个请求任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, object value);       
        /// <summary>
        /// 执行一个请求任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, AsyncCallback asyncCallback, int timeout, object value);
    }

/// <summary>
    /// 基础协议请求接口
    /// </summary>
    public class DataHandleBase:IDataHandleBase
    {
        /// <summary>
        /// 执行一个任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="value"></param>
        public void RequestReactorFunction(int functionType,  object value)
        {
            RequestReactorFunction(functionType, null, NetworkConstants.WAIT_FOR_COMPLETE, value);
        }    
        /// <summary>
        /// 执行一个请求任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        public virtual void RequestReactorFunction(int functionType, AsyncCallback asyncCallback, int timeout, object value)
        {
            ConnectionTask task = new ConnectionTask(functionType.ToString(), value);
            ((ReactorConnectionAdapter)ProtocolProxyAgent.Instance().reactorConnectionAdapter).ExecuteTaskAsync(task, asyncCallback, timeout);
        }
    }
	
```





