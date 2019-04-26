using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 服务已经运行或者未完全结束
    /// </summary>
    public class PoolNotStopException : Exception
    {
        public PoolNotStopException() : base("服务已经运行或者未完全结束") { }
        public PoolNotStopException(string message) : base(message) { }
    }
}
