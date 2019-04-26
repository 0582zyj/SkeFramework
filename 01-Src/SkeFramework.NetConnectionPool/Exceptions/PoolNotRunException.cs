using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{
    /// <summary>
    /// 服务未启动
    /// </summary>
    public class PoolNotRunException : Exception
    {
        public PoolNotRunException() : base("服务未启动") { }
        public PoolNotRunException(string message) : base(message) { }
    }
}
