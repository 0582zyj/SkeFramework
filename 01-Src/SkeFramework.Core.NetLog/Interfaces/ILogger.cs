using SkeFramework.Core.NetLog.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.NetLog.Interfaces
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void Write(LogLevel level, string msg, params object[] args);
    }
}
