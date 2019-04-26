using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 服务状态错误
    /// </summary>
    public class StateException : Exception
    {
        public StateException() : base("服务状态错误") { }
        public StateException(string message) : base(message) { }
    }
}
