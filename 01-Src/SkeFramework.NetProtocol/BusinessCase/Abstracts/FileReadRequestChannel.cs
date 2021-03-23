using SkeFramework.NetProtocol.DataFrame;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.DataFrame;
using SkeFramework.NetSerialPort.Topology;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.BusinessCase.Abstracts
{
    /// <summary>
    /// 读文件基本实现
    /// </summary>
    internal class FileReadRequestChannel : SingleRequestChannel
    {
        /// <summary>
        /// 总帧数
        /// </summary>
        protected int frameCount = 0;
        /// <summary>
        /// 当前第几帧
        /// </summary>
        protected short frameIndex = 1;
        /// <summary>
        /// 文件包
        /// </summary>
        protected PacketFrameBase packet;

        public FileReadRequestChannel(ReactorBase reactor, string controlCode)
     : this(reactor, null, controlCode)
        {
        }
        public FileReadRequestChannel(ReactorBase reactor, INode endPoint, string controlCode)
            : base(reactor, endPoint, controlCode)
        {
            packet = new SHSerialPacket(-1);
        }



        /// <summary>
        /// 创建发送帧[如需调整重发次数需重写ExecuteTaskSync]
        /// </summary>
        /// <param name="connectionTask"></param>
        /// <returns></returns>
        protected override NetworkData CreateNetworkData(ConnectionTask connectionTask)
        {
            if (connectionTask.Param == null || !(connectionTask.Param is int))
                return null;
            this.frameCount = Convert.ToInt16(connectionTask.Param);
            return this.CreateSendFrameIndexData(this.frameIndex);
        }
        /// <summary>
        /// 接收处理
        /// </summary>
        /// <param name="data"></param>
        public override void OnReceive(NetworkData data)
        {
            int frameCount = -1;
            SHSerialFrame serialFrame = new SHSerialFrame(data.Buffer);
            if (serialFrame.ParseToFrame(data.Buffer) == FrameBase.ResultOfParsingFrame.ReceivingCompleted
                && !serialFrame.IsErrorCmd)
            {
                using (MemoryStream Stream = new MemoryStream(serialFrame.getFrameText(), 0, serialFrame.getFrameText().Length))
                {
                    using (BinaryReader reader = new BinaryReader(Stream))
                    {
                        short readFrameIndex = reader.ReadInt16();
                        int frameLength = serialFrame.getTextLenght() - 2;
                        byte[] realFrameData = reader.ReadBytes(frameLength);
                        packet.AddOrUpdateFrameBase(readFrameIndex, realFrameData);
                        this.frameIndex = (short)(readFrameIndex + 1);
                    }
                }
                if (this.frameIndex <= this.frameCount)
                {//获取下一帧
                    NetworkData networkData = CreateSendFrameIndexData(this.frameIndex);
                    if (networkData == null)
                        return;
                    CaseSendFrame(networkData, NetworkConstants.DefaultTaskInterval, NetworkConstants.DefaultTaskCount);
                }
                else
                {
                    //接受完成
                }
            }
            data.ResultData = frameCount;
            base.OnReceive(data);
        }
        /// <summary>
        /// 超时处理
        /// </summary>
        public override void StopReceiveInternal()
        {
            base.StopReceiveInternal();
        }

        /// <summary>
        /// 发送第几帧数据
        /// </summary>
        /// <param name="sendFrameIndex"></param>
        protected virtual NetworkData CreateSendFrameIndexData(short sendFrameIndex)
        {
            if (sendFrameIndex > this.frameCount)
                return null;
            byte[] frameNumByte = new byte[2];
            frameNumByte=BitConverter.GetBytes(sendFrameIndex);
            FrameBase frame = this.CreateSHSerialFrame(frameNumByte);
            return NetworkData.Create(this.Local, frame.FrameBytes, frame.FrameBytes.Length);
        }
    }
}