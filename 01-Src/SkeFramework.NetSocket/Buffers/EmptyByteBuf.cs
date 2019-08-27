using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers.Constant;

namespace SkeFramework.NetSocket.Buffers
{
    public class EmptyByteBuf : IByteBuf
    {

        public EmptyByteBuf(IByteBufAllocator allocator)
        {
            this.Allocator = allocator;
        }
        public int Capacity => throw new NotImplementedException();

        public ByteOrder Order => throw new NotImplementedException();

        public int MaxCapacity => throw new NotImplementedException();

        public  IByteBufAllocator Allocator { get; }

        public int ReaderIndex => throw new NotImplementedException();

        public int WriterIndex => throw new NotImplementedException();

        public int ReadableBytes => throw new NotImplementedException();

        public int WritableBytes => throw new NotImplementedException();

        public int MaxWritableBytes => throw new NotImplementedException();

        public bool HasArray => throw new NotImplementedException();

        public byte[] Array => throw new NotImplementedException();

        public bool IsDirect => throw new NotImplementedException();

        public int ArrayOffset => throw new NotImplementedException();

        public int IoBufferCount => throw new NotImplementedException();

        public int ReferenceCount => throw new NotImplementedException();

        public IByteBuf AdjustCapacity(int newCapacity)
        {
            throw new NotImplementedException();
        }

        public IByteBuf Clear()
        {
            throw new NotImplementedException();
        }

        public IByteBuf Compact()
        {
            throw new NotImplementedException();
        }

        public IByteBuf CompactIfNecessary()
        {
            throw new NotImplementedException();
        }

        public IByteBuf Copy()
        {
            throw new NotImplementedException();
        }

        public IByteBuf Copy(int index, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf DiscardReadBytes()
        {
            throw new NotImplementedException();
        }

        public IByteBuf DiscardSomeReadBytes()
        {
            throw new NotImplementedException();
        }

        public IByteBuf Duplicate()
        {
            throw new NotImplementedException();
        }

        public IByteBuf EnsureWritable(int minWritableBytes)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int index)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int index)
        {
            throw new NotImplementedException();
        }

        public IByteBuf GetBytes(int index, IByteBuf destination)
        {
            throw new NotImplementedException();
        }

        public IByteBuf GetBytes(int index, IByteBuf destination, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf GetBytes(int index, IByteBuf destination, int dstIndex, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf GetBytes(int index, byte[] destination)
        {
            throw new NotImplementedException();
        }

        public IByteBuf GetBytes(int index, byte[] destination, int dstIndex, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int index)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int index)
        {
            throw new NotImplementedException();
        }

        public int GetInt(int index)
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte> GetIoBuffer()
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte> GetIoBuffer(int index, int length)
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte>[] GetIoBuffers()
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte>[] GetIoBuffers(int index, int length)
        {
            throw new NotImplementedException();
        }

        public long GetLong(int index)
        {
            throw new NotImplementedException();
        }

        public short GetShort(int index)
        {
            throw new NotImplementedException();
        }

        public uint GetUnsignedInt(int index)
        {
            throw new NotImplementedException();
        }

        public ushort GetUnsignedShort(int index)
        {
            throw new NotImplementedException();
        }

        public bool IsReadable()
        {
            throw new NotImplementedException();
        }

        public bool IsReadable(int size)
        {
            throw new NotImplementedException();
        }

        public bool IsWritable()
        {
            throw new NotImplementedException();
        }

        public bool IsWritable(int size)
        {
            throw new NotImplementedException();
        }

        public IByteBuf MarkReaderIndex()
        {
            throw new NotImplementedException();
        }

        public IByteBuf MarkWriterIndex()
        {
            throw new NotImplementedException();
        }

        public bool ReadBoolean()
        {
            throw new NotImplementedException();
        }

        public byte ReadByte()
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadBytes(int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadBytes(IByteBuf destination)
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadBytes(IByteBuf destination, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadBytes(IByteBuf destination, int dstIndex, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadBytes(byte[] destination)
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadBytes(byte[] destination, int dstIndex, int length)
        {
            throw new NotImplementedException();
        }

        public char ReadChar()
        {
            throw new NotImplementedException();
        }

        public double ReadDouble()
        {
            throw new NotImplementedException();
        }

        public int ReadInt()
        {
            throw new NotImplementedException();
        }

        public long ReadLong()
        {
            throw new NotImplementedException();
        }

        public short ReadShort()
        {
            throw new NotImplementedException();
        }

        public IByteBuf ReadSlice(int length)
        {
            throw new NotImplementedException();
        }

        public uint ReadUnsignedInt()
        {
            throw new NotImplementedException();
        }

        public ushort ReadUnsignedShort()
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

        public IByteBuf ResetReaderIndex()
        {
            throw new NotImplementedException();
        }

        public IByteBuf ResetWriterIndex()
        {
            throw new NotImplementedException();
        }

        public IReferenceCounted Retain()
        {
            throw new NotImplementedException();
        }

        public IReferenceCounted Retain(int increment)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetBoolean(int index, bool value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetByte(int index, int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetBytes(int index, IByteBuf src)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetBytes(int index, IByteBuf src, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetBytes(int index, IByteBuf src, int srcIndex, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetBytes(int index, byte[] src)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetBytes(int index, byte[] src, int srcIndex, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetChar(int index, char value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetDouble(int index, double value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetIndex(int readerIndex, int writerIndex)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetInt(int index, int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetLong(int index, long value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetReaderIndex(int readerIndex)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetShort(int index, int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetUnsignedInt(int index, uint value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetUnsignedShort(int index, int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SetWriterIndex(int writerIndex)
        {
            throw new NotImplementedException();
        }

        public IByteBuf SkipBytes(int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf Slice()
        {
            throw new NotImplementedException();
        }

        public IByteBuf Slice(int index, int length)
        {
            throw new NotImplementedException();
        }

        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }

        public string ToString(Encoding encoding)
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

        public IByteBuf Unwrap()
        {
            throw new NotImplementedException();
        }

        public IByteBuf WithOrder(ByteOrder order)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteBoolean(bool value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteByte(int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteBytes(IByteBuf src)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteBytes(IByteBuf src, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteBytes(IByteBuf src, int srcIndex, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteBytes(byte[] src)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteBytes(byte[] src, int srcIndex, int length)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteChar(char value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteDouble(double value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteInt(int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteLong(long value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteShort(int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteUnsignedInt(uint value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteUnsignedShort(int value)
        {
            throw new NotImplementedException();
        }

        public IByteBuf WriteZero(int length)
        {
            throw new NotImplementedException();
        }
    }
}
