using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetProtocol.BusinessCase.Requests
{
    /// <summary>
    /// 搜索指令
    /// </summary>
    public class BroadcastSearchRequest : RefactorRequestChannel
    {
        public BroadcastSearchRequest(ReactorBase reactor)
        : this(reactor, null)
        {
        }

        public BroadcastSearchRequest(ReactorBase reactor, INode endPoint)
            : base(reactor, endPoint)
        {
        }

        public override void Configure(IConnectionConfig config)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteTaskSync(ConnectionTask connectionTask)
        {
            byte[] sendByte =( connectionTask.Param as List<byte>).ToArray();// Encoding.Default.GetBytes(message);
            NetworkData frame = NetworkData.Create(this.Local, sendByte, sendByte.Length);
            CaseSendFrame(frame, NetworkConstants.DefaultTaskInterval, NetworkConstants.WAIT_FOR_COMPLETE);

        }
        public override void BeginReceiveInternal()
        {
            throw new NotImplementedException();
        }


       

    }
}
