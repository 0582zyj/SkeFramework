using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 连接资源已经被分配并且不允许重复引用。
    /// </summary>
    public class AllotAndRepeatExecption : AllotExecption
    {
        public AllotAndRepeatExecption() : base("连接资源已经被分配并且不允许重复引用") { }
        public AllotAndRepeatExecption(string message) : base(message) { }
    }
}
