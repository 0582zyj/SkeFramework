using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetProtocol.BusinessCase.Requests;
using SkeFramework.NetProtocol.Constants;
using SkeFramework.NetProtocol.DataFrame;

namespace SkeFramework.NetProtocol
{
    public sealed class ProtocolUT : ReactorConnectionAdapter
    {
        /// <summary>
        /// 规约基类，具体实现根据工厂方法实例化
        /// </summary>
        private IConnection commCase_Receive = null;
        private IConnection commCase_Send = null;
        ///// <summary>
        ///// ErLangLinkCase 跟云主机Erlang进行通信【30S】 
        ///// </summary>
        //private ErLangLinkCase linkCase = null;
      
        public ProtocolUT(string ProtocolName, ReactorBase reactor)
            : base(reactor)
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialize()
        {
            //if (linkCase != null)
            //{
            //    linkCase.Task.Dead = true;
            //    linkCase.Dead = true;
            //}
            //linkCase = new ErLangLinkCase(new ProtocolTask("linkCase", null), this);
            //linkCase.SendFrameStart();
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
                FrameBase frame = new FrameBase();
                byte cmdByte = 0x01;// Convert.ToByte((task.Param as DataPoint).GetFunctionId());
                commCase_Send = this.connectionDocker.GetCase(cmdByte) as IConnection;
                if (commCase_Send == null)
                {
                    int taskname = Convert.ToInt32(task.Name);
                    switch (taskname)//应答
                    {
                        case ProtocolConst.APP_BROADCAST_SEARCH_NEW_HOST:
                            commCase_Send = new BroadcastSearchRequest(this._reactor);
                            break;
                    }
                }
                else
                {
                    //commCase_Send.Task.Param = task.Param;
                    //commCase_Send.Task.Complete(TaskState.Processing);
                }
                task.SetRelatedProtocol(commCase_Send);
                ((RefactorRequestChannel)commCase_Send).ExecuteTaskSync(task);
            }
            catch (Exception ex)
            {
                string msg = String.Format("协议收到任务，此函数被调用。 TaskName:{0}; Message:{1}", task.Name, ex.Message);
                Console.WriteLine(msg);
                task.Dead = true;
            }
        }
        /// <summary>
        /// 处理返回的数据
        /// </summary>
        /// <param name="networkData"></param>
        protected override void ProcessReceivedData(NetworkData networkData)
        {
            base.ProcessReceivedData(networkData);
            IConnection connection = this.connectionDocker.GetCase(networkData.RemoteHost);
            if (connection!=null && connection is RefactorRequestChannel)
            {
                RefactorRequestChannel requestChannel = (RefactorRequestChannel)connection;
                requestChannel.InvokeReceiveIfNotNull(networkData);
                commCase_Receive = new RefactorProxyResponseChannel(this._reactor, requestChannel);
                ((RefactorResponseChannel)commCase_Receive).OnReceive(networkData);
            }
        }
    }
}
