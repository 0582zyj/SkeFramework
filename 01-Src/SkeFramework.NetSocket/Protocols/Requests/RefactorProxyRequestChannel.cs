using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net.Reactor;
using SkeFramework.NetSocket.Protocols.Configs;
using SkeFramework.NetSocket.Protocols.Connections;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Protocols.Requests
{
    /// <summary>
    /// 请求代理类
    /// </summary>
   public class RefactorProxyRequestChannel : RefactorRequestChannel
    {
        public RefactorProxyRequestChannel(ReactorBase reactor, string controlCode)
            : this(reactor, reactor.LocalEndpoint, controlCode)
        {
        }

        public RefactorProxyRequestChannel(ReactorBase reactor, INode endPoint, string controlCode)
            : base(reactor,  endPoint, controlCode)
        {
        }

        public override void BeginReceiveInternal()
        {
            this.Receiving = false;
            Sender.EndSend();
        }

        public override void Configure(IConnectionConfig config)
        {
            this.connectionConfig = config;
        }

        public override void ExecuteTaskSync(ConnectionTask connectionTask)
        {
        }
    }
}
