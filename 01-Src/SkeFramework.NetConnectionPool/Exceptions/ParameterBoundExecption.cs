using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 参数范围错误
    /// </summary>
    public class ParameterBoundExecption : Exception
    {
        public ParameterBoundExecption() : base("参数范围错误") { }
        public ParameterBoundExecption(string message) : base(message) { }
    }
}
