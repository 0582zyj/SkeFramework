using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 连接池已经饱和，不能提供连接
    /// </summary>
    public class PoolFullException : Exception
    {
        public PoolFullException() : base("连接池已经饱和，不能提供连接") { }
        public PoolFullException(string message) : base(message) { }
    }
}
