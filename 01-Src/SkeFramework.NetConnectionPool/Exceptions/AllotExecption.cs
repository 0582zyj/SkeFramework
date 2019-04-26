using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{


    /// <summary>
    /// 连接资源不可以被分配。
    /// </summary>
    public class AllotExecption : Exception
    {
        public AllotExecption() : base("连接资源不可以被分配。") { }
        public AllotExecption(string message) : base(message) { }
    }
}
