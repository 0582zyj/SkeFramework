using SkeFramework.NetSerialPort.Buffers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Buffers.EndianBinary
{
     /// <summary>
     /// 大端小端帮助类
     /// </summary>
    public static class EndianSwaper
    {
      
        public const int DefaultEndian = (int)ByteOrder.BigEndian;

        /// <summary>
        /// 和系统默认大小端比较，如果不一致就需要转换
        /// </summary>
        /// <param name="Endian"></param>
        /// <returns></returns>
        public static bool NeedSwapEndian(int Endian)
        {
            return Endian != SystemIsLittleEndian;
        }
        public static int SystemIsLittleEndian
        {
            get
            {
                return BitConverter.IsLittleEndian ? (int)ByteOrder.LittleEndian : (int)ByteOrder.BigEndian; 
            }
        }
        /// <summary>
        /// 翻转指定长度的数组顺序
        /// </summary>
        /// <param name="dataBuffer"></param>
        /// <param name="Size"></param>
        private static void Swap(byte[] dataBuffer, int Size)
        {
            for (int i = 0; i < (Size >> 1); i++)
            {
                byte tmp = dataBuffer[i];
                dataBuffer[i] = dataBuffer[Size - i - 1];
                dataBuffer[Size - i - 1] = tmp;
            }
        }
        /// <summary>
        /// 短整形转换
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static short EndianSwap(short Data)
        {
            byte[] dataBuff = BitConverter.GetBytes(Data);
            Swap(dataBuff, sizeof(short));
            return BitConverter.ToInt16(dataBuff, 0);
        }
        /// <summary>
        /// 无符号短整形转换
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ushort EndianSwap(ushort Data)
        {
            byte[] dataBuff = BitConverter.GetBytes(Data);
            Swap(dataBuff, sizeof(ushort));
            return BitConverter.ToUInt16(dataBuff, 0);
        }
        /// <summary>
        /// 整形转换
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int EndianSwap(int Data)
        {
            byte[] dataBuff = BitConverter.GetBytes(Data);
            Swap(dataBuff, sizeof(int));
            return BitConverter.ToInt32(dataBuff, 0);
        }
        /// <summary>
        /// 无符号整形转换
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static uint EndianSwap(uint Data)
        {
            byte[] dataBuff = BitConverter.GetBytes(Data);
            Swap(dataBuff, sizeof(uint));
            return BitConverter.ToUInt32(dataBuff, 0);
        }
        /// <summary>
        /// 长整形转换
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static long EndianSwap(long Data)
        {
            byte[] dataBuff = BitConverter.GetBytes(Data);
            Swap(dataBuff, sizeof(long));
            return BitConverter.ToInt64(dataBuff, 0);
        }
        /// <summary>
        /// 无符号长整形转换
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ulong EndianSwap(ulong Data)
        {
            byte[] dataBuff = BitConverter.GetBytes(Data);
            Swap(dataBuff, sizeof(ulong));
            return BitConverter.ToUInt64(dataBuff, 0);
        }
    }
}
