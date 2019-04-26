using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{


    /// <summary>
    /// 连接资源耗尽，或错误的访问时机。
    /// </summary>
    public class OccasionExecption : Exception
    {
        public OccasionExecption() : base("连接资源耗尽，或错误的访问时机。") { }
        public OccasionExecption(string message) : base(message) { }
    }
}
