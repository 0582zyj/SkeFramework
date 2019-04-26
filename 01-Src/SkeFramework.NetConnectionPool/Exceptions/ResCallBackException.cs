using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{
    /// <summary>
    /// 连接池资源未全部回收
    /// </summary>
    public class ResCallBackException : Exception
    {
        public ResCallBackException() : base("连接池资源未全部回收") { }
        public ResCallBackException(string message) : base(message) { }
    }
}
