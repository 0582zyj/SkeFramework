using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net.Bootstrap
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
        IConnection NewConnection(INode remoteEndpoint);
        IConnection NewConnection(INode localEndpoint, INode remoteEndpoint);
    }
}
