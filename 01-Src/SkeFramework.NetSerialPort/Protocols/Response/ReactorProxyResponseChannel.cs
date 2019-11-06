using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Response
{
    /// <summary>
    /// 响应代理类
    /// </summary>
    public class ReactorProxyResponseChannel : ReactorResponseChannel
    {
        public ReactorProxyResponseChannel(ReactorBase reactor, SerialPort outboundSocket)
            : this(reactor, outboundSocket,null)
        {
        }

        public ReactorProxyResponseChannel(ReactorBase reactor, SerialPort outboundSocket, INode endPoint)
            : base(reactor, outboundSocket, endPoint)
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
