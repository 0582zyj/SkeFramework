using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Interfaces
{
    public delegate void NotificationSuccessDelegate<TNotification>(TNotification notification) where TNotification : INotification;
    public delegate void NotificationFailureDelegate<TNotification>(TNotification notification, AggregateException exception) where TNotification : INotification;
    public delegate void NotificationConnectionDelegate<TNotification>(TNotification notification) where TNotification : INotification;
    /// <summary>
    /// 推送链接接口
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IPushConnection<TNotification> where TNotification : INotification
    {
        /// <summary>
        /// 发送接口
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task Send(TNotification notification);
    }
}
