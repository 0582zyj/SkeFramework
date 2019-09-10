using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Net.Reactor
{
    public abstract class ProxyReactorBase : ReactorBase
    {
        protected Dictionary<INode, ReactorResponseChannel> SocketMap = new Dictionary<INode, ReactorResponseChannel>();

        protected ProxyReactorBase(IPAddress localAddress, int localPort, 
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            SocketType socketType = SocketType.Stream, ProtocolType protocol = ProtocolType.Tcp,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(localAddress, localPort, encoder, decoder, allocator, socketType, protocol, bufferSize)
        {
            BufferSize = bufferSize;
        }



        /// <summary>
        ///     If true, proxies created for each inbound connection share the parent's thread-pool. If false, each proxy is
        ///     allocated
        ///     its own thread pool.
        ///     Defaults to true.
        /// </summary>
        public bool ProxiesShareFiber { get; protected set; }

        protected override void ReceivedData(NetworkData availableData, ReactorResponseChannel responseChannel)
        {
            responseChannel.OnReceive(availableData);
        }
    }
}