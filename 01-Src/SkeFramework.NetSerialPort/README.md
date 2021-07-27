# SkeFramework.NetSerialPort

#### ����
SkeFramework.NetSerialPort�Ǵ��Ϳ��ٿ����������ڴ���ͨ��ʵ�ֵ�ͨ�ſ⣬��ͨ���ÿ������ɴ������ͨ�ſ�����

#### ����ʵ������

![](https://p3-tt.byteimg.com/origin/pgc-image/33fc4b9c3067438a9167322003c28893?from=pc)


#### ��װ�̳�

1.  Package Manager=>Install-Package SkeFramework.NetSerialPort -Version 1.0.2.1

#### ʹ��˵��

1.  ����������SkeFramework.NetSerialPort�⣬Ŀǰ�ÿ���δ������Nuget������������ͨ����ϵ���˻�������Դ��������ɣ��汾��Ŀǰ��V1.02.01.
2.  ʵ��ReactorConnectionAdapter��ParsingReceivedData��ԭʼ���ݽ�����GetConnection�������������ӷ��䡿 ProcessReceivedData��Э�����ݴ���ProcessTask����������
3.  ������ԼЭ������ࡾProtocolProxyAgent��-��������������ServerBootstrap���ٶԴ��ڼ��乤���̵߳Ŀ����͹رգ����˻�����ͨ��DefaultChannelPromiseʵ�ֶ���������ص�������ɾ�����ݼ����Ļص����������Լʵ�֡�BusinessCase����ͨ���̳�Ĭ�������ࡾRefactorProxyRequestChannel��������ʵ������ʵ�����á�Configure����ִ�з�������ExecuteTaskSync����������֡��CreateNetworkData�������մ���OnReceive������ʱ����StopReceiveInternal��
4.  ����֡�ࡾDataFrame��������ͨ�Ź�Լ��ʽ��ͨ�Ż��ࡾFrameBase��ʵ������֡��ķ�װ���ɸ��ݾ���ҵ��������ӿڲ�;���Э�������ݽ���ʵ�塣
5.  ʵ��ReactorConnectionAdapter��ParsingReceivedData��ԭʼ���ݽ�����GetConnection�������������ӷ��䡿 ProcessReceivedData��Э�����ݴ���ProcessTask����������
6.  ����ӿڲ㡾DataHandle��������ʵ��ҵ��̳л����ӿڡ�IDataHandleBase���ͻ���ʵ���ࡾDataHandleBase��������һ��������ͨ��ProtocolProxyAgent����������ʽ�ﵽ�������Э���ͨ�š�

#### ����ʹ������

![](https://p6-tt.byteimg.com/origin/pgc-image/3b855f708ab94f4f86f85056461b9231?from=pc)

ProtocolUT

```
public sealed class ProtocolUT : ReactorConnectionAdapter
    {
        /// <summary>
        /// ��Լ���࣬����ʵ�ָ��ݹ�������ʵ����
        /// </summary>
        private IConnection commCase_Send = null;
      
      
        public ProtocolUT(string ProtocolName, ReactorBase reactor)
            : base(reactor)
        {

        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void Initialize()
        {
         
        }
        /// <summary>
        /// Э��һ�յ����񣬴˺����������á�
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
                    switch (taskname)//Ӧ��
                    {
                        case ProtocolConst.APP_BROADCAST_SEARCH_NEW_HOST:
                            commCase_Send = new BroadcastSearchRequest(this._reactor);
                            break;
                        default:
                            string msg = String.Format("Э��δʵ�֡� TaskName:{0}; ", task.Name);
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
                string msg = String.Format("Э���յ����񣬴˺��������á� TaskName:{0}; Message:{1}", task.Name, ex.Message);
                Console.WriteLine(msg);
                task.Dead = true;
            }
        }
        /// <summary>
        /// ԭʼ���ݽ���
        /// </summary>
        /// <param name="OriginalBuffer"></param>
        /// <returns></returns>
        public override FrameBase ParsingReceivedData(byte[] OriginalBuffer)
        {
            Console.WriteLine("�����յ�����<<--" + this.Encoder.ByteEncode(OriginalBuffer));
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
        /// ��������
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
    /// Э�������
    /// </summary>
    public class ProtocolProxyAgent
    {
        #region ����ģʽ
        /// <summary>
        /// Э�������
        /// </summary>
        private static ProtocolProxyAgent mSingleInstance;
        /// <summary>
        /// ����ģʽ
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
        /// ͨ�Ż���
        /// </summary>
        private IReactor reactor;
        /// <summary>
        /// Э��������
        /// </summary>
        private IConnection connectionAdapter;
        /// <summary>
        /// Э��������
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
        /// Э���������
        /// </summary>
        private IChannelPromise channelListenser = new DefaultChannelPromise();

        #region Э�鴦�����
        /// <summary>
        /// �˿��Ƿ��
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
        /// ����һ��ͨ��
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
        /// ֹͣ
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

        #region ������Ϣ��������
        public void DataPointListener_Send(NetworkData networkData, IConnection requestChannel)
        {
            string content = requestChannel.Encoder.ByteEncode(networkData.Buffer);
        }
        public void DataPointListener_Receive(NetworkData incomingData, IConnection responseChannel)
        {
            channelListenser.OnReceivedDataPoint(incomingData, responseChannel.RemoteHost.TaskTag);
        }
        /// <summary>
        /// ���һ��������
        /// </summary>
        /// <param name="listener"></param>
        public void AddDataPointListener(IChannelListener listener)
        {
            channelListenser.AddDataPointListener(listener);
        }
        /// <summary>
        /// �Ƴ�һ��������
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
    /// ��֡����Э��
    /// </summary>
    internal class SingleRequestChannel: RefactorProxyRequestChannel
    {
        /// <summary>
        /// �������
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
        /// ��������֡
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
        /// ��������֡
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
IDataHandleBase��DataHandleBase

```
/// <summary>
    /// ����Э������ӿ�
    /// </summary>
    public interface IDataHandleBase
    {
        /// <summary>
        /// ִ��һ����������
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, object value);       
        /// <summary>
        /// ִ��һ����������
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, AsyncCallback asyncCallback, int timeout, object value);
    }

/// <summary>
    /// ����Э������ӿ�
    /// </summary>
    public class DataHandleBase:IDataHandleBase
    {
        /// <summary>
        /// ִ��һ������
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="value"></param>
        public void RequestReactorFunction(int functionType,  object value)
        {
            RequestReactorFunction(functionType, null, NetworkConstants.WAIT_FOR_COMPLETE, value);
        }    
        /// <summary>
        /// ִ��һ����������
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





