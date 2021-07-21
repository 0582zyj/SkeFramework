using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Buffers.EndianBinary
{
    /// <summary>
    /// 字节流工具
    /// </summary>
    public class BinaryConverter
    {
        public static BinaryReader GetBufferReader(byte[] Buff)
        {
            return GetBufferReader(Buff, 0, Buff.Length);
        }

        public static BinaryReader GetBufferReader(byte[] Buff, int Start, int Length)
        {
            MemoryStream Stream = new MemoryStream(Buff, Start, Length);
            return new EndianBinaryReader(Stream);
        }

        public static BinaryWriter GetBufferWriter(byte[] Buff)
        {
            return GetBufferWriter(Buff, 0, Buff.Length);
        }

        public static BinaryWriter GetBufferWriter(byte[] Buff, int Start, int Length)
        {
            MemoryStream Stream = new MemoryStream(Buff, Start, Length);
            return new EndianBinaryWriter(Stream);
        }
    }
}
