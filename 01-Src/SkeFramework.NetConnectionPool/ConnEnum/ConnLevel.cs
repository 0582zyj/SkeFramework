using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.ConnService
{
    /// <summary>
    /// 要申请连接的级别
    /// </summary>
    public enum ConnLevel
    {
        /// <summary>
        /// 独占方式，分配全新的连接资源，并且该连接资源在本次使用释放回连接池之前不能在分配出去。如果连接池只能分配引用记数类型连接资源则该级别将产生一个异常，标志连接池资源耗尽
        /// </summary>
        ReadOnly,
        /// <summary>
        /// 优先级-高，分配全新的连接资源，不使用引用记数技术。注：此级别不保证在分配后该连接资源后，仍然保持独立占有资源，若想独立占有资源请使用ReadOnely
        /// </summary>
        High,
        /// <summary>
        /// 优先级-中，适当应用引用记数技术分配连接
        /// </summary>
        None,
        /// <summary>
        /// 优先级-底，尽可能使用引用记数技术分配连接
        /// </summary>
        Bottom
    }
}
