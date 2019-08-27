using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net.Bootstrap;
using SkeFramework.NetSocket.Reactor;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net
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
    }
}
