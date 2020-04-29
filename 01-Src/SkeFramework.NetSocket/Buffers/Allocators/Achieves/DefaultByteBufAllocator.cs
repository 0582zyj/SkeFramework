using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers.ByteBuffers.Achieves;

namespace SkeFramework.NetSocket.Buffers.Allocators.Achieves
{
    /// <summary>
    /// 默认字节缓冲处理器
    /// </summary>
    public class DefaultByteBufAllocator : IByteBufAllocator
    {
        private readonly IByteBuf _emptyBuf;

        public DefaultByteBufAllocator()
        {
            _emptyBuf = new EmptyByteBuf(this);
        }

        public IByteBuf Buffer()
        {
            return DirectBuffer();
        }

        public IByteBuf Buffer(int initialCapcity)
        {
            return DirectBuffer(initialCapcity);
        }

        public IByteBuf Buffer(int initialCapacity, int maxCapacity)
        {
            return DirectBuffer(initialCapacity, maxCapacity);
        }

        #region Range validation

        private static void Validate(int initialCapacity, int maxCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException("initialCapacity", "initialCapacity must be greater than zero");

            if (initialCapacity > maxCapacity)
                throw new ArgumentOutOfRangeException("initialCapacity",
                    string.Format("initialCapacity ({0}) must be greater than maxCapacity ({1})", initialCapacity,
                        maxCapacity));
        }

        #endregion

        #region Direct buffer methods

        protected IByteBuf DirectBuffer()
        {
            return DirectBuffer(256, int.MaxValue);
        }

        protected IByteBuf DirectBuffer(int initialCapacity)
        {
            return DirectBuffer(initialCapacity, int.MaxValue);
        }

        protected IByteBuf DirectBuffer(int initialCapacity, int maxCapacity)
        {
            if (initialCapacity == 0 && maxCapacity == 0)
                return _emptyBuf;

            Validate(initialCapacity, maxCapacity);

            return new EmptyByteBuf(this);
        }


        #endregion
    }
}
