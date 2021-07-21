using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Bootstrap;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Net.Udp;
using SkeFramework.NetSerialPort.Topology;

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
            return new UdpProxyReactor(listenAddress, Encoder, Decoder, Allocator);
        }
    }
}
