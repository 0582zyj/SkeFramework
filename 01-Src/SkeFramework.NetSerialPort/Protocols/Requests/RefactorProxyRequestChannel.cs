using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Requests
{
    /// <summary>
    /// 请求代理类
    /// </summary>
   public class RefactorProxyRequestChannel : RefactorRequestChannel
    {
        public RefactorProxyRequestChannel(ReactorBase reactor)
            : this(reactor,  null)
        {
        }

        public RefactorProxyRequestChannel(ReactorBase reactor, INode endPoint)
            : base(reactor,  endPoint)
        {
        }

        public override void BeginReceiveInternal()
        {
        }

        public override void Configure(IConnectionConfig config)
        {
        }

        public override void ExecuteTaskSync(ConnectionTask connectionTask)
        {
        }
    }
}
