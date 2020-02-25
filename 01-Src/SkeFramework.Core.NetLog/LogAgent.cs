using SkeFramework.Core.NetLog.Constants;
using SkeFramework.Core.NetLog.Interfaces;
using SkeFramework.Core.NetLog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.NetLog
{
    /// <summary>
    /// 日志代理
    /// </summary>
     public static class LogAgent
    {
        static readonly object loggerLock = new object();
        static List<ILogger> loggers { get; set; }
        static Dictionary<CounterToken, Stopwatch> counters;

        static LogAgent()
        {
            counters = new Dictionary<CounterToken, Stopwatch>();
            loggers = new List<ILogger>();
            AddLogger(new ConsoleLogger());
        }
        /// <summary>
        /// 新增一个日志实现
        /// </summary>
        /// <param name="logger"></param>
        public static void AddLogger(ILogger logger)
        {
            lock (loggerLock)
                loggers.Add(logger);
        }
        /// <summary>
        /// 清楚日志实现
        /// </summary>
        public static void ClearLoggers()
        {
            lock (loggerLock)
                loggers.Clear();
        }
        /// <summary>
        /// 获取日志实现列表
        /// </summary>
        public static IEnumerable<ILogger> Loggers
        {
            get { return loggers; }
        }
        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(LogLevel level, string msg, params object[] args)
        {
            lock (loggers)
            {
                foreach (var log in loggers)
                    log.Write(level, msg, args);
            }
        }
        /// <summary>
        /// 输出Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Info(string msg, params object[] args)
        {
            Write(LogLevel.Info, msg, args);
        }
        /// <summary>
        /// 输出Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Debug(string msg, params object[] args)
        {
            Write(LogLevel.Debug, msg, args);
        }
        /// <summary>
        /// 输出Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Error(string msg, params object[] args)
        {
            Write(LogLevel.Error, msg, args);
        }
        /// <summary>
        /// 开始计时
        /// </summary>
        /// <returns></returns>
        public static CounterToken StartCounter()
        {
            var t = new CounterToken
            {
                Id = Guid.NewGuid().ToString()
            };

            var sw = new Stopwatch();

            counters.Add(t, sw);

            sw.Start();

            return t;
        }
        /// <summary>
        /// 结束计时
        /// </summary>
        /// <param name="counterToken"></param>
        /// <returns></returns>
        public static TimeSpan StopCounter(CounterToken counterToken)
        {
            if (!counters.ContainsKey(counterToken))
                return TimeSpan.Zero;

            var sw = counters[counterToken];

            sw.Stop();

            counters.Remove(counterToken);

            return sw.Elapsed;
        }
        /// <summary>
        /// 结束计时并打印日志
        /// </summary>
        /// <param name="counterToken"></param>
        /// <param name="msg"></param>
        /// <param name="level"></param>
        public static void StopCounterAndLog(CounterToken counterToken, string msg, LogLevel level = LogLevel.Info)
        {
            var elapsed = StopCounter(counterToken);
            if (!msg.Contains("{0}"))
                msg += " {0}";
            LogAgent.Write(level, msg, elapsed.TotalMilliseconds);
        }
    }

}
