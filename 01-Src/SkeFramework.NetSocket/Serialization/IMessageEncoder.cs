﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Net;

namespace SkeFramework.NetSocket.Serialization
{
    /// <summary>
    ///     Used to encode <see cref="NetworkData" /> inside Helios
    /// </summary>
    public interface IMessageEncoder
    {
        /// <summary>
        ///     Encodes <see cref="buffer" /> into a format that's acceptable for <see cref="IConnection" />.
        ///     Might return a list of encoded objects in <see cref="encoded" />, and it's up to the handler to determine
        ///     what to do with them.
        /// </summary>
        void Encode(IConnection connection, IByteBuf buffer, out List<IByteBuf> encoded);

        /// <summary>
        ///     Creates a deep clone of this <see cref="IMessageEncoder" />, including
        /// </summary>
        IMessageEncoder Clone();
    }

    public abstract class MessageEncoderBase : IMessageEncoder
    {
        public abstract void Encode(IConnection connection, IByteBuf buffer, out List<IByteBuf> encoded);
        public abstract IMessageEncoder Clone();
    }

    /// <summary>
    ///     Does nothing - default encoder
    /// </summary>
    public class NoOpEncoder : MessageEncoderBase
    {
        public override void Encode(IConnection connection, IByteBuf buffer, out List<IByteBuf> encoded)
        {
            encoded = new List<IByteBuf> { buffer };
        }

        public override IMessageEncoder Clone()
        {
            return new NoOpEncoder();
        }
    }
}
