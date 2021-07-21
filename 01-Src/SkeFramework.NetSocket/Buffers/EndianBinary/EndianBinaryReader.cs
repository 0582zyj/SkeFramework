using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Buffers.EndianBinary
{
    /// <summary>
    /// 字节流读【大小端】
    /// </summary>
    public class EndianBinaryReader : BinaryReader
    {
        private int _endian = EndianSwaper.DefaultEndian;

        public EndianBinaryReader(Stream input) : base(input)
        {
        }

        public EndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }
        /// <summary>
        /// 短整形读取
        /// </summary>
        /// <returns></returns>
        public override short ReadInt16()
        {
            short value = base.ReadInt16();
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            return value;
        }
        /// <summary>
        /// 无符号短整形读取
        /// </summary>
        /// <returns></returns>
        public override ushort ReadUInt16()
        {
            ushort value = base.ReadUInt16();
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            return value;
        }
        /// <summary>
        /// 整形读取
        /// </summary>
        /// <returns></returns>
        public override int ReadInt32()
        {
            int value = base.ReadInt32();
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            return value;
        }
        /// <summary>
        /// 无符号整形读取
        /// </summary>
        /// <returns></returns>
        public override uint ReadUInt32()
        {
            uint value = base.ReadUInt32();
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            return value;
        }
        /// <summary>
        /// 长整形读取
        /// </summary>
        /// <returns></returns>
        public override long ReadInt64()
        {
            long value = base.ReadInt64();
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            return value;
        }
        /// <summary>
        /// 无符号长整形读取
        /// </summary>
        /// <returns></returns>
        public override ulong ReadUInt64()
        {
            ulong value = base.ReadUInt64();
            if (EndianSwaper.NeedSwapEndian(_endian))
            {
                value = EndianSwaper.EndianSwap(value);
            }
            return value;
        }
        /// <summary>
        /// 关闭
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
