using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net.Bootstrap.Server;
using SkeFramework.NetSocket.Reactor;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net
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
            if (ConnectionEstablishedCallback != null)
                reactor.OnConnection += (ConnectionEstablishedCallback)ConnectionEstablishedCallback.Clone();
            if (ConnectionTerminatedCallback != null)
                reactor.OnDisconnection += (ConnectionTerminatedCallback)ConnectionTerminatedCallback.Clone();
            if (ExceptionCallback != null)
                reactor.OnError += (ExceptionCallback)ExceptionCallback.Clone();

            return reactor;
        }

        public IConnection NewConnection()
        {
            return NewConnection(Node.Any());
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
