using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Protocols;

namespace SkeFramework.NetSocket.Buffers
{
    /// <summary>
    ///     Used to encode <see cref="NetworkData" /> inside Helios
    /// </summary>
    public interface IMessageDecoder
    {
        /// <summary>
        ///     Encodes <see cref="buffer" /> into a format that's acceptable for <see cref="IConnection" />.
        ///     Might return a list of decoded objects in <see cref="decoded" />, and it's up to the handler to determine
        ///     what to do with them.
        /// </summary>
        void Decode(IConnection connection, IByteBuf buffer, out List<IByteBuf> decoded);

        /// <summary>
        ///     Creates a deep clone of this <see cref="IMessageDecoder" /> instance with the exact same settings as the parent.
        /// </summary>
        /// <returns></returns>
        IMessageDecoder Clone();
    }
}
