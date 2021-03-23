using SkeFramework.NetProtocol.DataFrame.BusinessFrames.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.DataFrame
{
    /// <summary>
    /// UT_BUS串口数据帧
    /// </summary>
    public class SHSerialFrame : SerialAbstractFrame
    {
        /// <summary>
        /// 物理地址默认3F，当做同步头
        /// </summary>
        public readonly static byte[] SynvHead = { 0x3F };
        public const int FrameLength_Fix = 4;
        public const int FrameIndex_SynvHead = 0;
        public const int FrameIndex_BodyLength = 2;
        public const int FrameIndex_BodyText = 3;

        public SHSerialFrame(byte[] frame) : base(frame, SynvHead.ToArray()) { }

        public SHSerialFrame(byte cmd, byte[] body)
            : base(null, SynvHead.ToArray())
        {
            this.setBodyLength(body.Length);
            this.setCmdByte(cmd);
            this.setFrameText(body);
        }

        public SHSerialFrame(int matchOffset, byte[] frame) : base(frame, SynvHead.ToArray()) { this.MatchOffset = matchOffset; }


        /// <summary>
        /// 设置数据正文
        /// </summary>
        /// <param name="body"></param>
        private void setFrameText(byte[] body)
        {
            if (body != null)
            {
                for (int i = 0; i < body.Length; i = i + 1)
                {
                    frameBytes[FrameIndex_BodyText + i] = body[i];
                }
            }
        }
        /// <summary>
        /// 获取数据正文
        /// </summary>
        /// <returns></returns>
        public byte[] getFrameText()
        {
            if (frameBytes != null && frameBytes.Length > FrameIndex_BodyText)
            {
                byte[] bytes = new byte[getTextLenght()];
                Array.Copy(frameBytes, FrameIndex_BodyText, bytes, 0, bytes.Length);
                return bytes;
            }
            return null;
        }
        /// <summary>
        /// 设置功能码
        /// </summary>
        /// <param name="cmd"></param>
        private void setCmdByte(byte cmd)
        {
            if (this.frameBytes.Length > FrameIndex_CmdByte)
            {
                frameBytes[FrameIndex_CmdByte] = cmd;
            }
        }
        /// <summary>
        /// 设置数据正文长度
        /// </summary>
        /// <param name="length"></param>
        private void setBodyLength(int length)
        {
            this.frameBytes = new byte[length + FrameLength_Fix];
            for (int i = 0; i < SyncHead.Count; i++)
            {
                frameBytes[FrameIndex_SynvHead + i] = SyncHead[i];
            }
            this.frameBytes[FrameIndex_BodyLength] = Convert.ToByte(length);
        }

        /// <summary>
        /// 获取数据正文长度
        /// </summary>
        /// <returns></returns>
        public int getBodyLength()
        {
            if (frameBytes != null && frameBytes.Length > FrameIndex_BodyLength)
            {
                return frameBytes[FrameIndex_BodyLength];
            }
            return 0;
        }

        /// <summary>
        /// 获取实际数据正文长度
        /// </summary>
        /// <returns></returns>
        public int getTextLenght()
        {
            if (frameBytes != null && frameBytes.Length - FrameLength_Fix > 0)
            {
                return frameBytes.Length - FrameLength_Fix;
            }
            return 0;
        }

        public new string ControlCode
        {
            get { return Convert.ToInt32(this.cmdByte & 0x7F).ToString(); }
            set { cmdByte = Convert.ToByte(value); }
        }
        public bool IsErrorCmd { get { return this.cmdByte >> 7 == 1; } }
    }
}
