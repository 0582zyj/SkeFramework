using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net.Reactor;
using SkeFramework.NetSocket.Protocols;
using SkeFramework.NetSocket.Protocols.Connections;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Bootstrap
{
    /// <summary>
    /// Socket服务端工厂
    /// </summary>
    public interface IServerFactory : IConnectionFactory
    {
        /// <summary>
        /// 新建一个通信类
        /// </summary>
        /// <param name="listenAddress"></param>
        /// <returns></returns>
        IReactor NewReactor(INode listenAddress);
        /// <summary>
        /// 新建一个通信类
        /// </summary>
        /// <param name="listenAddress"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        IReactor NewReactor(INode listenAddress, IConnection connection);
    }
}
