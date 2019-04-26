using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.ConnService

{
    /// <summary>
    /// 连接池状态
    /// </summary>
    public enum PoolState
    {
        /// <summary>
        /// 刚刚创建的对象，表示该对象未被调用过StartSeivice方法。
        /// </summary>
        UnInitialize,
        /// <summary>
        /// 初始化中，该状态下服务正在按照参数初始化连接池。
        /// </summary>
        Initialize,
        /// <summary>
        /// 运行中
        /// </summary>
        Run,
        /// <summary>
        /// 停止状态
        /// </summary>
        Stop
    }
}
