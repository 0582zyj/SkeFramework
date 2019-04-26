using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 一个key对象只能申请一个连接
    /// </summary>
    public class KeyExecption : Exception
    {
        public KeyExecption() : base("一个key对象只能申请一个连接") { }
        public KeyExecption(string message) : base(message) { }
    }
}
