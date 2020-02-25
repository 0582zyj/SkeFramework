using SkeFramework.Core.NetLog.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.NetLog.Globals
{
    public static class CounterExtensions
    {
        public static void StopAndLog(this CounterToken counterToken, string msg, LogLevel level = LogLevel.Info)
        {
            LogAgent.StopCounterAndLog(counterToken, msg, level);
        }

        public static TimeSpan Stop(this CounterToken counterToken)
        {
            return LogAgent.StopCounter(counterToken);
        }
    }

}
