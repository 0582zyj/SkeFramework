using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Buffers.Allocators
{
    /// <summary>
    /// 字节缓冲区分配程序接口
    /// </summary>
    public interface IByteBufAllocator
    {
        IByteBuf Buffer();

        IByteBuf Buffer(int initialCapcity);

        IByteBuf Buffer(int initialCapacity, int maxCapacity);
    }
}
