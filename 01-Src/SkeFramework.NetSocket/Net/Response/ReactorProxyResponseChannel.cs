using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Channels;
using SkeFramework.NetSocket.Reactor;

namespace SkeFramework.NetSocket.Net.Response
{
    /// <summary>
    ///     Response channel receives all of its events directly from the <see cref="ReactorBase" /> and doesn't maintain any
    ///     internal buffers,
    ///     nor does it directly interact with its socket in any way
    /// </summary>
    public class ReactorProxyResponseChannel : ReactorResponseChannel
    {
        public ReactorProxyResponseChannel(ReactorBase reactor, Socket outboundSocket, NetworkEventLoop eventLoop)
            : base(reactor, outboundSocket, eventLoop)
        {
        }

        public ReactorProxyResponseChannel(ReactorBase reactor, Socket outboundSocket, IPEndPoint endPoint,
            NetworkEventLoop eventLoop)
            : base(reactor, outboundSocket, endPoint, eventLoop)
        {
        }

        public override void Configure(IConnectionConfig config)
        {
        }

        protected override void BeginReceiveInternal()
        {
        }

        protected override void StopReceiveInternal()
        {
        }
    }
}