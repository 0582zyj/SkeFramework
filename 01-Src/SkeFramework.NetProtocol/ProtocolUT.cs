using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.DataFrame;
using SkeFramework.NetSerialPort.Protocols.Requests;
using System;
using System.Text;
using SkeFramework.NetProtocol.BusinessCase.Requests;
using SkeFramework.NetProtocol.Constants;

namespace SkeFramework.NetProtocol
{
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
}
