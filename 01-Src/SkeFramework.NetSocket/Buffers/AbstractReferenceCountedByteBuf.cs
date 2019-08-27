using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Exceptions;

namespace SkeFramework.NetSocket.Buffers
{
    public abstract class AbstractReferenceCountedByteBuf : AbstractByteBuf
    {
        private volatile int _refCount = 1;

        protected AbstractReferenceCountedByteBuf(int maxCapacity) : base(maxCapacity)
        {
        }

        public override int ReferenceCount => _refCount;

        /// <summary>
        ///     An unsafe operation designed to be used by a subclass that sets the reference count of the buffer directly
        /// </summary>
        /// <param name="refCount">The new <see cref="ReferenceCount" /> value to use.</param>
        protected void SetReferenceCount(int refCount)
        {
            _refCount = refCount;
        }

        public override IReferenceCounted Retain()
        {
            while (true)
            {
                var refCount = _refCount;
                if (refCount == 0)
                    throw new IllegalReferenceCountException(0, 1);
                if (refCount == int.MaxValue)
                    throw new IllegalReferenceCountException(int.MaxValue, 1);

                if (Interlocked.CompareExchange(ref _refCount, refCount + 1, refCount) == refCount)
                    break;
            }
            return this;
        }

        public override IReferenceCounted Retain(int increment)
        {
            Contract.Requires(increment > 0);

            while (true)
            {
                var refCount = _refCount;
                if (refCount == 0)
                    throw new IllegalReferenceCountException(0, increment);
                if (refCount > int.MaxValue - increment)
                    throw new IllegalReferenceCountException(refCount, increment);

                if (Interlocked.CompareExchange(ref _refCount, refCount + increment, refCount) == refCount)
                    break;
            }
            return this;
        }

        public override IReferenceCounted Touch()
        {
            return this;
        }

        public override IReferenceCounted Touch(object hint)
        {
            return this;
        }

        public override bool Release()
        {
            while (true)
            {
                var refCount = _refCount;
                if (refCount == 0)
                    throw new IllegalReferenceCountException(0, -1);

                if (Interlocked.CompareExchange(ref _refCount, refCount - 1, refCount) == refCount)
                {
                    if (refCount == 1)
                    {
                        Deallocate();
                        return true;
                    }
                    return false;
                }
            }
        }

        public override bool Release(int decrement)
        {
            Contract.Requires(decrement > 0);

            while (true)
            {
                var refCount = _refCount;
                if (refCount < decrement)
                    throw new IllegalReferenceCountException(refCount, decrement);

                if (Interlocked.CompareExchange(ref _refCount, refCount - decrement, refCount) == refCount)
                {
                    if (refCount == decrement)
                    {
                        Deallocate();
                        return true;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        ///     Called once <see cref="ReferenceCount" /> is equal to <c>0</c>.
        /// </summary>
        protected abstract void Deallocate();
    }
}