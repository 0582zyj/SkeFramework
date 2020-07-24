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

        public byte[] ByteDecoder(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return new byte[0];
            }
            List<byte> data = new List<byte>();
            message = message.Replace(" ", "");
            int length= message.Length / 2;
            for (int i = 0; i < length; i++)
            {
                data.Add((byte)(0xff & Convert.ToInt32(message.Substring(i * 2,  2), 16)));
            }
            return data.ToArray();
        }
    }
}
