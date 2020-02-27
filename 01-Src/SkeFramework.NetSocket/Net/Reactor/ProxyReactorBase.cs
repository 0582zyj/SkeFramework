using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Buffers.Allocators;
using SkeFramework.NetSocket.Protocols;
using SkeFramework.NetSocket.Protocols.Constants;
using SkeFramework.NetSocket.Protocols.Requests;
using SkeFramework.NetSocket.Protocols.Response;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net.Reactor
{
    public abstract class ProxyReactorBase : ReactorBase
    {
        protected Dictionary<string, IConnection> SocketMap = new Dictionary<string, IConnection>();

        protected ProxyReactorBase(INode listener, 
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(listener, encoder, decoder, allocator,  bufferSize)
        {
            BufferSize = bufferSize;
        }

        /// <summary>
        ///  If true, proxies created for each inbound connection share the parent's thread-pool. If false, each proxy is
        ///  allocated
        ///     its own thread pool.
        ///     Defaults to true.
        /// </summary>
        public bool ProxiesShareFiber { get; protected set; }

        protected override void ReceivedData(NetworkData availableData, RefactorResponseChannel responseChannel)
        {
            responseChannel.OnReceive(availableData);
        }
    }
}