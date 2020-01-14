using SkeFramework.NetSerialPort.Protocols.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Protocols.DataFrame
{
    /// <summary>
    /// 接受帧处理
    /// </summary>
   public class FrameBase
    {
        /// <summary>
        /// 帧接收时，解析数据到一个阶段时返回的结果。
        /// </summary>
        public enum ResultOfParsingFrame
        {
            /// <summary>
            /// 同步头错误
            /// </summary>
            SyncHeadError = -7,
            /// <summary>
            /// 帧超时
            /// </summary>
            ReceivedOverTime = -6,
            /// <summary>
            /// 已接收到部分数据，等待后面新接收到的数据。
            /// </summary>
            WaitingForNextData = -5,
            /// <summary>
            /// 收到了部分帧头数据。
            /// </summary>
            ReceivedPartHead = -4,
            /// <summary>
            /// 帧控制部分校验错误。
            /// </summary>
            ControlCheckError = -3,
            /// <summary>
            /// 帧数据校验错误。
            /// </summary>
            CrcCheckError = -2,
            /// <summary>
            /// 帧结构不匹配，如帧头不对。
            /// </summary>
            FormatNotMatched = -1,
            /// <summary>
            /// 接收到一个有效帧。
            /// </summary>
            ReceivingCompleted = 0
        }
        /// <summary>
        /// 当前解析状态（正在解析那一部分数据）。
        /// </summary>
        protected enum ParsingState
        {
            /// <summary>
            /// 正在解析帧头。
            /// </summary>
            ParsingHead,
            /// <summary>
            /// 正在解析帧控制部分。
            /// </summary>
            ParsingControl,
            /// <summary>
            /// 正在解析除帧头和控制部分以外的部分。
            /// </summary>
            ParsingRemainder,
            /// <summary>
            /// 帧解析完成，已接收到一个有效帧。
            /// </summary>
            ParsingFinished
        }

        /// <summary>
        /// 数据帧
        /// </summary>
        protected byte[] frameBytes = null;
        /// <summary>
        /// 获得一个帧的数据。
        /// </summary>
        public byte[] FrameBytes
        {
            get { return frameBytes; }
            set { frameBytes = value; }
        }
        /// <summary>
        /// 同步头
        /// </summary>
        protected List<byte> SyncHead;
        /// <summary>
        /// 协议控制码
        /// </summary>
        protected byte cmdByte;
        public string ControlCode
        {
            get { return Convert.ToInt32(this.cmdByte).ToString(); }
            set { cmdByte = Convert.ToByte( value); }
        }
        public FrameBase(byte[] data,byte[] syncHead)
        {
            this.frameBytes = data;
            this.SyncHead = syncHead == null?new List<byte>(): syncHead.ToList();
        }

        


        /// <summary>
        /// 校验帧数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual ResultOfParsingFrame ParseToFrame(byte[] data)
        {
            ResultOfParsingFrame re = ResultOfParsingFrame.FormatNotMatched;
            frameBytes = data;
            //这里可以校验同步头
            if(SyncHead!=null && SyncHead.Count > 0)
            {
                for (int i = 0; i < SyncHead.Count; i++)
                {
                    if (data[i] != SyncHead[i])
                    {
                        return ResultOfParsingFrame.SyncHeadError;
                    }
                }
            }
            re = ResultOfParsingFrame.ReceivingCompleted;
            return re;
        }

        public virtual ResultOfParsingFrame ParseToFrame(NetworkData data)
        {
            ResultOfParsingFrame re = ResultOfParsingFrame.FormatNotMatched;
            frameBytes = data.Buffer;
            //这里可以校验同步头
            if (SyncHead != null && SyncHead.Count > 0)
            {
                for (int i = 0; i < SyncHead.Count; i++)
                {
                    if (data.Buffer[i] != SyncHead[i])
                    {
                        return ResultOfParsingFrame.SyncHeadError;
                    }
                }
            }
            re = ResultOfParsingFrame.ReceivingCompleted;
            return re;
        }

    }
}
