using SkeFramework.Push.Core.Listenser.Interfaces;
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
    /// 用于处理接收数据的委托
    /// </summary>
    public delegate void ReceivedDataCallback<TNotification>(TNotification sender, IPushConnection<TNotification> responseChannel) where TNotification : INotification;
    /// <summary>
    /// 推送链接接口
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IPushConnection<TNotification>: IChannelPromise where TNotification : INotification
    {
        event ReceivedDataCallback<TNotification> OnReceivedDataCallback;
        /// <summary>
        /// 发送接口
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task Send(TNotification notification);
        /// <summary>
        /// 获取连接标识
        /// </summary>
        /// <returns></returns>
        string GetTag();
    }
}
