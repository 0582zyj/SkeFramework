using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Protocols.Constants
{
    /// <summary>
    /// 静态变量
    /// </summary>
    public static class NetworkConstants
    {
        /// <summary>
        /// 默认缓冲大小
        /// </summary>
        public const int DEFAULT_BUFFER_SIZE = 1024 * 32; //32k
        /// <summary>
        /// 默认积压大小
        /// </summary>
        public const int DefaultBacklog = 5;
        /// <summary>
        /// 内存端口
        /// </summary>
        public const int InMemoryPort = 0;
        /// <summary>
        ///  默认节点恢复间隔
        /// </summary>
        public static readonly TimeSpan DefaultNodeRecoveryInterval = TimeSpan.FromSeconds(30);
        /// <summary>
        /// 默认连接超时
        /// </summary>
        public static readonly TimeSpan DefaultConnectivityTimeout = TimeSpan.FromSeconds(30);
        /// <summary>
        /// -1代表等待超时
        /// </summary>
        public const int WAIT_FOR_COMPLETE = -1;
        /// <summary>
        ///  默认任务重发间隔时间[毫秒]
        /// </summary>
        public const int DefaultTaskInterval =1000;
        /// <summary>
        /// 默认消息解析超时时间[秒]
        /// </summary>
        public const long DefaultPraseTimeOut = 10;

        /// <summary>
        /// 检查健康状况
        /// </summary>
        public static readonly TimeSpan[] BackoffIntervals =
        {
            TimeSpan.FromSeconds(5), //5 seconds
            TimeSpan.FromSeconds(30), //30 seconds
            TimeSpan.FromMinutes(5), //5 minutes
            TimeSpan.FromMinutes(15), //15 minutes
            TimeSpan.FromMinutes(30), //30 minutes
            TimeSpan.FromHours(1), //1 hour
            TimeSpan.FromHours(2), //2 hours
            TimeSpan.FromHours(4), //4 hours
            TimeSpan.FromHours(12), //12 hours
            TimeSpan.FromDays(1), //1 day
            TimeSpan.FromDays(2) //2 days
        };
    }
}
