using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net.Reactor;
using SkeFramework.NetSocket.Protocols.Configs;
using SkeFramework.NetSocket.Protocols.Requests;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Protocols.Response
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
