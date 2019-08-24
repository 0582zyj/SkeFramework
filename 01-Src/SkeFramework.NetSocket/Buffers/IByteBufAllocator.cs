using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Buffers
{
    /// <summary>
    ///     Thread-safe interface for allocating <see cref="IByteBuf" /> instances for use inside Helios reactive I/O
    /// </summary>
    public interface IByteBufAllocator
    {
        IByteBuf Buffer();

        IByteBuf Buffer(int initialCapcity);

        IByteBuf Buffer(int initialCapacity, int maxCapacity);
    }
}
