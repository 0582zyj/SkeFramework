using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols;

namespace SkeFramework.NetSerialPort.Buffers.Serialization.Achieves
{
    public class NoOpEncoder : IMessageEncoder
    {
        public  void Encode(IConnection connection, IByteBuf buffer, out List<IByteBuf> encoded)
        {
            encoded = new List<IByteBuf> { buffer };
        }

        public IMessageEncoder Clone()
        {
            return new NoOpEncoder();
        }
    }
}
