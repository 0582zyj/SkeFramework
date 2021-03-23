using SkeFramework.NetProtocol.DataFrame;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.DataFrame;
using SkeFramework.NetSerialPort.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.BusinessCase.Abstracts
{
    /// <summary>
    /// 文件多帧传输
    /// </summary>
    internal class FileWriteRequestChannel:SingleRequestChannel
    {
        /// <summary>
        /// 文件包
        /// </summary>
        protected SHSerialPacket sHSerialPacket;
        /// <summary>
        /// 帧次数
        /// </summary>
        protected int frameIndex = 1;

        public FileWriteRequestChannel(ReactorBase reactor, string controlCode)
        : this(reactor, null, controlCode)
        {
        }
        public FileWriteRequestChannel(ReactorBase reactor, INode endPoint, string controlCode)
            : base(reactor, endPoint, controlCode)
        {
        }
       
        /// <summary>
        /// 创建发送帧[如需调整重发次数需重写ExecuteTaskSync]
        /// </summary>
        /// <param name="connectionTask"></param>
        /// <returns></returns>
        protected  override NetworkData CreateNetworkData(ConnectionTask connectionTask)
        {
            if (connectionTask.Param == null)
                return null;
            string fileName = connectionTask.Param.ToString();
            this.sHSerialPacket = new SHSerialPacket(SHSerialPacket.SwitchPacketFrameSize);
            SHSerialPacket.CreateSerialPackage<SHSerialPacket>(this.sHSerialPacket, fileName);
            if (this.sHSerialPacket == null)
                return null;
            return this.CreateSendFrameIndexData((short)this.frameIndex);
        }
        /// <summary>
        /// 接收处理
        /// </summary>
        /// <param name="data"></param>
        public override void OnReceive(NetworkData data)
        {
            SHSerialFrame serialFrame = new SHSerialFrame(data.Buffer);
            if (serialFrame.ParseToFrame(data.Buffer) == FrameBase.ResultOfParsingFrame.ReceivingCompleted
                && !serialFrame.IsErrorCmd)
            {
                byte[] bodyText = serialFrame.getFrameText();
                Array.Reverse(bodyText);
                this.frameIndex = BitConverter.ToInt16(bodyText, 0) + 1;
                Console.WriteLine("NextFrameIndex:" + frameIndex);
                data.ResultData = this.frameIndex;
                if (this.frameIndex <= this.sHSerialPacket.FrameCount)
                {
                    //发送下一帧数据
                    NetworkData networkData = CreateSendFrameIndexData((short)this.frameIndex);
                    if (networkData == null)
                        return;
                    CaseSendFrame(networkData, NetworkConstants.DefaultTaskInterval, NetworkConstants.DefaultTaskCount);
                }
            }
            base.OnReceive(data);
        }
        /// <summary>
        /// 发送第几帧数据
        /// </summary>
        /// <param name="sendFrameIndex"></param>
        protected virtual NetworkData CreateSendFrameIndexData(short sendFrameIndex)
        {
            if (sendFrameIndex > this.sHSerialPacket.FrameCount)
                return null;
            List<byte> sendByte = new List<byte>();
            byte[] frameNumByte = BitConverter.GetBytes(sendFrameIndex);
            Array.Reverse(frameNumByte);
            sendByte.AddRange(frameNumByte);
            FrameBase frameBase = this.sHSerialPacket.GetFrameBase(sendFrameIndex);
            sendByte.AddRange(frameBase.FrameBytes);
            FrameBase frame = this.CreateSHSerialFrame(sendByte.ToArray());
            return NetworkData.Create(this.Local, frame.FrameBytes, frame.FrameBytes.Length);
        }

    }
}
