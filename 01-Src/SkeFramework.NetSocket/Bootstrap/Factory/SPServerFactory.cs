using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers.Serialization;
using SkeFramework.NetSocket.Net;
using SkeFramework.NetSocket.Net.Reactor;
using SkeFramework.NetSocket.Net.SerialPorts;
using SkeFramework.NetSocket.Net.Udp;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Bootstrap
{
    /// <summary>
    /// UDP服务端工厂具体实现
    /// </summary>
    public sealed class SPServerFactory : ServerFactoryBase
    {
        public SPServerFactory(ServerBootstrap other)
            : base(other)
        {
        }

        protected override ReactorBase NewReactorInternal(INode listenAddress)
        {
            switch (listenAddress.reactorType)
            {
                case ReactorType.SerialPorts:
                    return new SerialPortReactor(listenAddress, Encoder, Decoder, Allocator);
                case ReactorType.Udp:
                    return new UdpProxyReactor(listenAddress, Encoder, Decoder, Allocator);
                default:
                    return null;
            }
        }
    }
}
