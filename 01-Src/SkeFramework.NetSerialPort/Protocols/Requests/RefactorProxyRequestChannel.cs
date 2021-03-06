﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Configs.Enums;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Requests
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
            if (config.HasOption(OptionKeyEnums.ProtocolTimeOut.ToString()))
            {
                int ProtocolTimeOut = (int)config.GetOption(OptionKeyEnums.ProtocolTimeOut.ToString());
                if (ProtocolTimeOut > -1 && ProtocolTimeOut < NetworkConstants.BackoffIntervals.Length)
                {
                    this.Timeout = NetworkConstants.BackoffIntervals[ProtocolTimeOut];
                }
            }
            
        }

        public override void ExecuteTaskSync(ConnectionTask connectionTask)
        {
        }
    }
}
