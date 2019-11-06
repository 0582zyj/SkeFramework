using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols;

namespace SkeFramework.NetSerialPort.Buffers.Serialization.Achieves
{
    public class NoOpDecoder : IMessageDecoder
    {
        public  void Decode(IConnection connection, IByteBuf buffer, out List<IByteBuf> decoded)
        {
            var outBuffer = connection.Allocator.Buffer(buffer.ReadableBytes);
            outBuffer.WriteBytes(buffer.ToArray(), buffer.ReaderIndex, buffer.ReadableBytes);
            decoded = new List<IByteBuf> { outBuffer };
        }

        public  IMessageDecoder Clone()
        {
            return new NoOpDecoder();
        }
    }
}
