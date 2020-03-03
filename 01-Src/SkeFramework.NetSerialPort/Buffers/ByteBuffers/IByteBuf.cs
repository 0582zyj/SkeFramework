using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Buffers.Constants;

namespace SkeFramework.NetSerialPort.Buffers
{
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
        /// 标志，指示这个是否由字节数组支持
        /// </summary>
        bool HasArray { get; }
        /// <summary>
        /// 获取整个数组
        /// </summary>
        /// <returns></returns>
        byte[] ToArray();

        byte[] ReadBytes(int length);

        IByteBuf WriteBytes(byte[] buffer, int index, int length);
    }
}
