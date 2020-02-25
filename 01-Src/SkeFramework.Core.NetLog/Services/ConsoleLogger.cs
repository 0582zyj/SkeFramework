using SkeFramework.Core.NetLog.Constants;
using SkeFramework.Core.NetLog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.NetLog.Services
{
    /// <summary>
    /// 控制台输出日志
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Write(LogLevel level, string msg, params object[] args)
        {
            var s = msg;

            if (args != null && args.Length > 0)
                s = string.Format(msg, args);
            var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ttt");
            switch (level)
            {
                case LogLevel.Info:
                    Console.Out.WriteLine(d + " [INFO] " + s);
                    break;
                case LogLevel.Debug:
                    Console.Out.WriteLine(d + " [DEBUG] " + s);
                    break;
                case LogLevel.Error:
                    Console.Error.WriteLine(d + " [ERROR] " + s);
                    break;
            }
        }
    }
}