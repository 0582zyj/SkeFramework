using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net.Bootstrap.Server;
using SkeFramework.NetSocket.Net.Udp;
using SkeFramework.NetSocket.Reactor;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net
{
    /// <summary>
    /// UDP服务端工厂具体实现
    /// </summary>
    public sealed class UdpServerFactory : ServerFactoryBase
    {
        public UdpServerFactory(ServerBootstrap other) : base(other)
        {
        }

        protected override ReactorBase NewReactorInternal(INode listenAddress)
        {
            return new UdpProxyReactor(listenAddress.Host, listenAddress.Port, EventLoop, Encoder, Decoder, Allocator,
                BufferBytes);
        }
    }
}
