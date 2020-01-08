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

        public string ByteEncode(byte[] buffer, int offset=0, int size=0)
        {
            if (size == 0)
            {
                size = buffer.Length;
            }
            StringBuilder ret = new StringBuilder();
            string tmp = "";
            for (int i = offset; i < size; ++i)
            {
                tmp = "0" + buffer[i].ToString("X");
                ret.Append(tmp.Substring(tmp.Length - 2, 2));
                ret.Append(" ");
            }
            return ret.ToString();
        }
    }
}
