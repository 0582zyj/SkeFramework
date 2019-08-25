using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Net
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