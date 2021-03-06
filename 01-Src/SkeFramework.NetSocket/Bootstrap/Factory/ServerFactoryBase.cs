﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeFramework.NetSocket.Net.Reactor;
using SkeFramework.NetSocket.Protocols;
using SkeFramework.NetSocket.Topology;
using SkeFramework.NetSocket.Topology.Nodes;

namespace SkeFramework.NetSocket.Bootstrap
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
            reactor.LocalEndpoint = listenAddress;
            if (ReceivedData != null)
                reactor.OnReceive += (ReceivedDataCallback)ReceivedData.Clone();
            return reactor;
        }
        public IReactor NewReactor(INode listenAddress, IConnection connection)
        {
            var reactor = NewReactorInternal(listenAddress);
            reactor.Configure(Config);
            reactor.LocalEndpoint = listenAddress;
            if (ReceivedData != null)
                reactor.OnReceive += (ReceivedDataCallback)ReceivedData.Clone();
            reactor.ConnectionAdapter = connection;
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

     

        #endregion

        /// <summary>
        /// 具体实现
        /// </summary>
        /// <param name="listenAddress"></param>
        /// <returns></returns>
        protected abstract ReactorBase NewReactorInternal(INode listenAddress);

      
    }
}