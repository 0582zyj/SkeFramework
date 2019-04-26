using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{
    /// <summary>
    /// 无法释放，不存在的key
    /// </summary>
    public class NotKeyExecption : Exception
    {
        public NotKeyExecption() : base("无法释放，不存在的key") { }
        public NotKeyExecption(string message) : base(message) { }
    }
}
