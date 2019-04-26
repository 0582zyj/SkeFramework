using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 连接资源已经失效。
    /// </summary>
    public class ResLostnExecption : Exception
    {
        public ResLostnExecption() : base("连接资源已经失效。") { }
        public ResLostnExecption(string message) : base(message) { }
    }
}
