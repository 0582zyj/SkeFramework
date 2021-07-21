﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers.Serialization;
using SkeFramework.NetSerialPort.Net;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Net.SerialPorts;
using SkeFramework.NetSerialPort.Net.Udp;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Bootstrap
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
