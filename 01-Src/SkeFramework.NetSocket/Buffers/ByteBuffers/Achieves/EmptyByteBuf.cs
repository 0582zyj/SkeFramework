using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers.Allocators;
using SkeFramework.NetSocket.Buffers.Constants;
using SkeFramework.NetSocket.Protocols.Constants;

namespace SkeFramework.NetSocket.Buffers.ByteBuffers.Achieves
{
    public class EmptyByteBuf : IByteBuf
    {

        /// <summary>
        ///     The buffer itself
        /// </summary>
        private readonly byte[] _buffer;
        /// <summary>
        /// 获取缓冲区最大大小
        /// </summary>
        public int Capacity
        {
            get {return _buffer.Length; }
        }
        /// <summary>
        /// 大小端排序
        /// </summary>
        public Constants.ByteOrder Order
        {
            get { return ByteOrder.BigEndian; }
        }
        /// <summary>
        /// 当前缓冲区可用最大大小
        /// </summary>
        public int MaxCapacity
        {
            get;
            set;
        }

        public IByteBufAllocator Allocator { get; set; }
        /// <summary>
        /// 当前读序号
        /// </summary>
        public int ReaderIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 当前写序号
        /// </summary>
        public int WriterIndex
        {
            get;
            set;
        }

        public  int ReadableBytes
        {
            get { return WriterIndex - ReaderIndex; }
        }

        public  int WritableBytes
        {
            get { return Capacity - WriterIndex; }
        }

        public  int MaxWritableBytes
        {
            get { return MaxCapacity - WriterIndex; }
        }

        public bool HasArray
        {
            get { return ReadableBytes >= 0; }
        }

        public byte[] ToArray()
        {
            return  _buffer.Take(ReadableBytes).ToArray();
        }


        public EmptyByteBuf(IByteBufAllocator allocator)
        {
            this.Allocator = allocator;
            this._buffer = new byte[NetworkConstants.DEFAULT_BUFFER_SIZE];
        }

        public virtual IByteBuf SetWriterIndex(int writerIndex)
        {
            if (writerIndex < ReaderIndex || writerIndex > Capacity)
                throw new IndexOutOfRangeException(
                    string.Format("WriterIndex: {0} (expected: 0 <= readerIndex({1}) <= writerIndex <= capacity ({2})",
                        writerIndex, ReaderIndex, Capacity));

            WriterIndex = writerIndex;
            return this;
        }

        public virtual IByteBuf SetReaderIndex(int readerIndex)
        {
            if (readerIndex < 0 || readerIndex > WriterIndex)
                throw new IndexOutOfRangeException(
                    string.Format("ReaderIndex: {0} (expected: 0 <= readerIndex <= writerIndex({1})", readerIndex,
                        WriterIndex));
            ReaderIndex = readerIndex;
            return this;
        }

        public virtual IByteBuf SetIndex(int readerIndex, int writerIndex)
        {
            if (readerIndex < 0 || readerIndex > writerIndex || writerIndex > Capacity)
                throw new IndexOutOfRangeException(
                    string.Format(
                        "ReaderIndex: {0}, WriterIndex: {1} (expected: 0 <= readerIndex <= writerIndex <= capacity ({2})",
                        readerIndex, writerIndex, Capacity));

            ReaderIndex = readerIndex;
            WriterIndex = writerIndex;
            return this;
        }

   

        public IByteBuf ReadBytes(IByteBuf destination)
        {
            return Allocator.Buffer();
        }

        public IByteBuf WriteBytes(byte[] buffer, int index, int length)
        {
            if (length > this.WritableBytes)
                throw new IndexOutOfRangeException(
                    string.Format("length({0}) exceeds src.readableBytes({1}) where src is: {2}", length,
                        this.WritableBytes, this));
            Array.Copy(buffer, index, this._buffer, WriterIndex, length);
            this.SetWriterIndex(this.ReaderIndex + length);
            return this; 
        }

        public int ReferenceCount
        {
            get { throw new NotImplementedException(); }
        }

        public IReferenceCounted Retain()
        {
            throw new NotImplementedException();
        }

        public IReferenceCounted Retain(int increment)
        {
            throw new NotImplementedException();
        }

        public IReferenceCounted Touch()
        {
            throw new NotImplementedException();
        }

        public IReferenceCounted Touch(object hint)
        {
            throw new NotImplementedException();
        }

        public bool Release()
        {
            throw new NotImplementedException();
        }

        public bool Release(int decrement)
        {
            throw new NotImplementedException();
        }
    }
}
