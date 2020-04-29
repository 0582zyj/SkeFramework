using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Interfaces
{
    public delegate void NotificationSuccessDelegate<TNotification>(TNotification notification) where TNotification : INotification;
    public delegate void NotificationFailureDelegate<TNotification>(TNotification notification, AggregateException exception) where TNotification : INotification;
    /// <summary>
    /// 发送推送接口
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IPushConnection<TNotification> where TNotification : INotification
    {
        Task Send(TNotification notification);
    }
}
