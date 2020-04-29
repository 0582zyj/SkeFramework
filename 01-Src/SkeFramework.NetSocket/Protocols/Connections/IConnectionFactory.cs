using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Protocols.Connections
{
    /// <summary>
    /// 连接工厂接口
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// 新建一个连接
        /// </summary>
        /// <returns></returns>
        IConnection NewConnection();
        /// <summary>
        /// 新建一个链接
        /// </summary>
        /// <param name="remoteEndpoint"></param>
        /// <returns></returns>
        IConnection NewConnection(INode remoteEndpoint);
    }
}
