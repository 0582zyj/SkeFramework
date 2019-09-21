﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetSerialPort.Topology.Nodes;

namespace SkeFramework.NetSerialPort.Bootstrap
{
    /// <summary>
    /// Sokcet服务端工厂抽象实现
    /// </summary>
    public abstract class ServerFactoryBase : ServerBootstrap, IServerFactory
    {
        protected ServerFactoryBase(ServerBootstrap other)
            : base(other)
        {
        }

        #region 实现接口
        public IReactor NewReactor(INode listenAddress)
        {
            var reactor = NewReactorInternal(listenAddress);
            reactor.Configure(Config);

            if (ReceivedData != null)
                reactor.OnReceive += (ReceivedDataCallback)ReceivedData.Clone();
            return reactor;
        }

        public IConnection NewConnection()
        {
            return NewConnection(Node.Empty());
        }

        public IConnection NewConnection(INode localEndpoint)
        {
            var reactor = (ReactorBase)NewReactor(localEndpoint);
            return reactor.ConnectionAdapter;
        }

        public IConnection NewConnection(INode localEndpoint, INode remoteEndpoint)
        {
            return NewConnection(localEndpoint);
        }
        #endregion

        /// <summary>
        /// 具体实现
        /// </summary>
        /// <param name="listenAddress"></param>
        /// <returns></returns>
        protected abstract ReactorBase NewReactorInternal(INode listenAddress);

    }
}