using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Workers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Services.Brokers
{
    /// <summary>
    /// 服务端推送反应堆
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public abstract class PushBroker<TRefactor,TNotification> : IPushBroker< TNotification> 
        where TRefactor : class
        where TNotification : INotification
    {
        /// <summary>
        /// 服务端类
        /// </summary>
        protected TRefactor refactor=null;

        static PushBroker()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.Expect100Continue = false;
        }

        public PushBroker(IPushConnectionFactory connectionFactory)
        {
            running = false;
            ServiceConnectionFactory = connectionFactory;
        }

        public event NotificationSuccessDelegate<TNotification> OnNotificationSucceeded;
        public event NotificationFailureDelegate<TNotification> OnNotificationFailed;
        public event NotificationConnectionDelegate<TNotification> OnConnection;

        /// <summary>
        /// 推送链接工厂
        /// </summary>
        protected IPushConnectionFactory ServiceConnectionFactory { get; set; }
        /// <summary>
        /// 推送线程容器管理
        /// </summary>
        protected WorkDocker<TNotification> WorkDocker;
        /// <summary>
        /// 是否运行
        /// </summary>
        private bool running;
        /// <summary>
        /// 默认工作线程数
        /// </summary>
        public int DefaultWorks;

        #region 启动和关闭
        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="connectionConfig"></param>
        public abstract void SetupParamOptions(IConnectionConfig connectionConfig);
        /// <summary>
        /// 启动服务端
        /// </summary>
        public void Start()
        {
            if (running)
                return;
            PushServerStart();
            running = true;
            WorkDocker = new WorkDocker<TNotification>(this,ServiceConnectionFactory);
            WorkDocker.ChangeScale(1);
        }
        /// <summary>
        /// 关闭服务端
        /// </summary>
        /// <param name="immediately"></param>
        public void Stop(bool immediately = false)
        {
            if (!running)
                throw new OperationCanceledException("ServiceBroker has already been signaled to Stop");
            running = false;
            WorkDocker.StopWorker(immediately);
            PushServerStop();
        }
        /// <summary>
        /// 服务端启动实际实现
        /// </summary>
        protected abstract void PushServerStart();
        /// <summary>
        /// 服务端关闭实际实现
        /// </summary>
        protected abstract void PushServerStop();
        #endregion
   
        #region 推送通知
        /// <summary>
        /// 推送成功通知
        /// </summary>
        /// <param name="notification"></param>
        public void RaiseNotificationSucceeded(TNotification notification)
        {
            OnNotificationSucceeded?.Invoke(notification);
        }
        /// <summary>
        /// 推送失败通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="exception"></param>
        public void RaiseNotificationFailed(TNotification notification, AggregateException exception)
        {
            OnNotificationFailed?.Invoke(notification, exception);
        }
        /// <summary>
        /// 新链接到达通知
        /// </summary>
        /// <param name="notification"></param>
        public void RaiseNewConnection(TNotification notification)
        {
            OnConnection?.Invoke(notification);
        }

        public TRefactor1 GetRefactorBroker<TRefactor1>() where TRefactor1 : class
        {
            if (this.refactor is TRefactor1)
            {
                return refactor as TRefactor1;
            }
            return default(TRefactor1);
        }
        #endregion
    }
}
