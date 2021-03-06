﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers.Allocators;
using SkeFramework.NetSocket.Buffers.Constants;

namespace SkeFramework.NetSocket.Buffers
{
    public interface IByteBuf : IReferenceCounted
    {
        int Capacity { get; }
        /// <summary>
        /// 大小端
        /// </summary>
        ByteOrder Order { get; }

        int MaxCapacity { get; }

        /// <summary>
        ///     The allocator who created this buffer
        /// </summary>
        IByteBufAllocator Allocator { get; }

        int ReaderIndex { get; }

        int WriterIndex { get; }

        int ReadableBytes { get; }

        int WritableBytes { get; }

        int MaxWritableBytes { get; }

        /// <summary>
        ///     Flag that indicates if this <see cref="IByteBuf" /> is backed by a byte array or not
        /// </summary>
        bool HasArray { get; }

        byte[] ToArray();

        IByteBuf ReadBytes(IByteBuf destination);

        IByteBuf WriteBytes(byte[] buffer, int index, int length);
    }
}
