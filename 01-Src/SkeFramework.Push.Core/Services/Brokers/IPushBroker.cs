using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Push.Interfaces
{
    /// <summary>
    /// 推送代理接口
    /// </summary>
    public interface IPushBroker<TNotification> where TNotification : INotification
    {
        event NotificationSuccessDelegate<TNotification> OnNotificationSucceeded;
        event NotificationFailureDelegate<TNotification> OnNotificationFailed;
        event NotificationConnectionDelegate<TNotification> OnConnection;
        /// <summary>
        /// 通知成功
        /// </summary>
        /// <param name="notification"></param>
        void RaiseNotificationSucceeded(TNotification notification);
        /// <summary>
        /// 通知失败
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="ex"></param>
        void RaiseNotificationFailed(TNotification notification, AggregateException ex);
        /// <summary>
        /// 新链接到达
        /// </summary>
        /// <param name="notification"></param>
        void RaiseNewConnection(TNotification notification);
        /// <summary>
        /// 开始
        /// </summary>
        void Start();
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="immediately"></param>
        void Stop(bool immediately = false);
        /// <summary>
        /// 设置启动参数
        /// </summary>
        /// <param name="connectionConfig"></param>
        void SetupParamOptions(IConnectionConfig connectionConfig);
        /// <summary>
        /// 获取原始反应核心
        /// </summary>
        /// <param name="connectionConfig"></param>
        TRefactor GetRefactorBroker<TRefactor>() where TRefactor : class;


    }
}
