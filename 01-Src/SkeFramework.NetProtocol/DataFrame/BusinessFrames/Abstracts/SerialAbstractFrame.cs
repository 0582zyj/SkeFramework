using SkeFramework.NetSerialPort.Protocols.DataFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.DataFrame.BusinessFrames.Abstracts
{
    /// <summary>
    /// 数据帧抽象实现
    /// </summary>
    public abstract class SerialAbstractFrame : FrameBase
    {
        public const int FrameIndex_CmdByte = 1;

        

        public SerialAbstractFrame() : this(null, null)
        {
        }
        public SerialAbstractFrame(byte[] data) : this(data, null)
        {
        }

        public SerialAbstractFrame(byte[] data, byte[] syncHead) : base(data, syncHead)
        {

        }

        /// <summary>
        /// 获取功能码
        /// </summary>
        /// <returns></returns>
        protected byte getCmdByte()
        {
            return this.frameBytes.Length > FrameIndex_CmdByte ? frameBytes[FrameIndex_CmdByte] : (byte)(0);
        }
        /// <summary>
        /// 设置校验码字节数据。
        /// </summary>
        public virtual void SetCheckBytes()
        {
           
        }

        /// <summary>
        /// 校验帧数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override ResultOfParsingFrame ParseToFrame(byte[] data)
        {
            if (data.Length == 0) return ResultOfParsingFrame.CrcCheckError;
            ResultOfParsingFrame result = base.ParseToFrame(data);
            if (result.Equals(ResultOfParsingFrame.ReceivingCompleted))
            {
                this.cmdByte = getCmdByte();
            }
            return result;
        }

    }
}

