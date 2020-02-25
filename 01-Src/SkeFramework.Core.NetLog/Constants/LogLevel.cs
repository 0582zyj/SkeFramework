using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.NetLog.Constants
{
    /// <summary>
    /// 日志等级
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        Info = 0,
        Debug = 2,
        Error = 8
    }
}
