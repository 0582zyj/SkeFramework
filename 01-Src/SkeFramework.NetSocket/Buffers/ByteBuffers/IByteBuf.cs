using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Buffers.Constants;

namespace SkeFramework.NetSerialPort.Buffers
{
    /// <summary>
    /// 字节接口
    /// </summary>
    public interface IByteBuf : IReferenceCounted
    {
        int Capacity { get; }
        /// <summary>
        /// 大小端
        /// </summary>
        ByteOrder Order { get; }
        /// <summary>
        /// 最大容量
        /// </summary>
        int MaxCapacity { get; }
        /// <summary>
        /// 创建此缓冲区的分配器
        /// </summary>
        IByteBufAllocator Allocator { get; }
        /// <summary>
        /// 可读序号
        /// </summary>
        int ReaderIndex { get; }
        /// <summary>
        /// 可写序号
        /// </summary>
        int WriterIndex { get; }
        /// <summary>
        /// 可读的字节
        /// </summary>
        int ReadableBytes { get; }
        /// <summary>
        /// 可写的字节个数
        /// </summary>
        int WritableBytes { get; }
        /// <summary>
        /// 最大可写入的字节数
        /// </summary>
        int MaxWritableBytes { get; }
        /// <summary>
        /// 当前消息接受时间
        /// </summary>
        long ReceiveTimeSpan { get; set; }
        /// <summary>
        /// 标志，指示这个是否由字节数组支持
        /// </summary>
        bool HasArray { get; }
        /// <summary>
        /// 获取整个数组
        /// </summary>
        /// <returns></returns>
        byte[] ToArray();
        /// <summary>
        /// 读取指令长度的字节
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] ReadBytes(int length);
        /// <summary>
        /// 写入指定长度的字节
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        IByteBuf WriteBytes(byte[] buffer, int index, int length);
    }
}
