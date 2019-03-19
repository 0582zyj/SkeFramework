using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 当前连接池状态不可以对属性赋值
    /// </summary>
    public class SetValueExecption : Exception
    {
        public SetValueExecption() : base("当前连接池状态不可以对属性赋值") { }
        public SetValueExecption(string message) : base(message) { }
    }
}
