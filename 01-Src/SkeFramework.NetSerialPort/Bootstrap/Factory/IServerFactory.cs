using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Bootstrap
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
