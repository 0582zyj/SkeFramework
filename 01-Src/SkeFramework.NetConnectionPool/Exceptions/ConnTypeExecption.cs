using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.Exceptions
{

    /// <summary>
    /// 无效的ConnTypeEnum类型参数
    /// </summary>
    public class ConnTypeExecption : Exception
    {
        public ConnTypeExecption() : base("无效的ConnTypeEnum类型参数") { }
        public ConnTypeExecption(string message) : base(message) { }
    }
}
