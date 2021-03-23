using SkeFramework.NetProtocol.DataFrame;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs.Enums;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.DataFrame;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.BusinessCase.Abstracts
{
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
}
