using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Buffers.EndianBinary
{
    /// <summary>
    /// 字节流写【大小端】
    /// </summary>
    public class EndianBinaryWriter : BinaryWriter
    {
        private int _endian = EndianSwaper.DefaultEndian;

        public EndianBinaryWriter(Stream output) : base(output)
        {
        }

        public EndianBinaryWriter(Stream output, Encoding encoding) : base(output, encoding)
        {
        }
        /// <summary>
        /// 短整形写
        /// </summary>
        /// <param name="value"></param>
        public override void Write(short value)
        {
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            base.Write(value);
        }
        /// <summary>
        /// 无符号短整形写
        /// </summary>
        /// <param name="value"></param>
        public override void Write(ushort value)
        {
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            base.Write(value);
        }
        /// <summary>
        /// 无符号整形写
        /// </summary>
        /// <param name="value"></param>
        public override void Write(uint value)
        {
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            base.Write(value);
        }
        /// <summary>
        /// 整形写
        /// </summary>
        /// <param name="value"></param>
        public override void Write(int value)
        {
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            base.Write(value);
        }
        /// <summary>
        /// 无符号长整形写
        /// </summary>
        /// <param name="value"></param>
        public override void Write(ulong value)
        {
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            base.Write(value);
        }
        /// <summary>
        /// 长整形写
        /// </summary>
        /// <param name="value"></param>
        public override void Write(long value)
        {
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            base.Write(value);
        }
        /// <summary>
        /// 关闭流
        /// </summary>
        public override void Close()
        {
            base.Close();
            if (null != this.BaseStream)
            {
                this.BaseStream.Close();
            }
        }
    }
}
