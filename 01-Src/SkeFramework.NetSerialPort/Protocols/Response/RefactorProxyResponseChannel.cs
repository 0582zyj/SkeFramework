using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Response
{
    /// <summary>
    /// 响应代理类
    /// </summary>
    public class RefactorProxyResponseChannel : RefactorResponseChannel
    {
        public RefactorProxyResponseChannel(ReactorBase reactor, RefactorRequestChannel request)
            : this(reactor, request, reactor.LocalEndpoint)
        {
        }

        public RefactorProxyResponseChannel(ReactorBase reactor, RefactorRequestChannel request, INode endPoint)
            : base(reactor, request, endPoint)
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
