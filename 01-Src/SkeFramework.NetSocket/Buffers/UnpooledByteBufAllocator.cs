using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Buffers
{
    public class UnpooledByteBufAllocator : AbstractByteBufAllocator
    {
        /// <summary>
        ///     Default instance
        /// </summary>
        public static readonly UnpooledByteBufAllocator Default = new UnpooledByteBufAllocator();

        protected override IByteBuf NewDirectBuffer(int initialCapacity, int maxCapacity)
        {
            return new UnpooledDirectByteBuf(this, initialCapacity, maxCapacity);
        }
    }
}
