using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{


    /// <summary>
    /// 引用记数已经为0。
    /// </summary>
    public class RepeatIsZeroExecption : Exception
    {
        public RepeatIsZeroExecption() : base("引用记数已经为0。") { }
        public RepeatIsZeroExecption(string message) : base(message) { }
    }
}
