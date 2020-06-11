using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
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
    public abstract class PushBroker<TNotification> : IPushBroker<TNotification> where TNotification : INotification
    {
        static PushBroker()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.Expect100Continue = false;
        }

        public PushBroker(IPushConnectionFactory<TNotification> connectionFactory)
        {
            ServiceConnectionFactory = connectionFactory;

            lockWorkers = new object();
            workers = new List<ServiceWorkerAdapter<TNotification>>();
            running = false;

            notifications = new BlockingCollection<TNotification>();
            ScaleSize = 1;
            //AutoScale = true;
            //AutoScaleMaxSize = 20;
        }

        public event NotificationSuccessDelegate<TNotification> OnNotificationSucceeded;
        public event NotificationFailureDelegate<TNotification> OnNotificationFailed;

        /// <summary>
        /// 推送工作线程数量
        /// </summary>
        public int ScaleSize { get; private set; }
        /// <summary>
        /// 推送链接工厂
        /// </summary>
        public IPushConnectionFactory<TNotification> ServiceConnectionFactory { get; set; }

        BlockingCollection<TNotification> notifications;
        List<ServiceWorkerAdapter<TNotification>> workers;
        object lockWorkers;
        bool running;

        public virtual void QueueNotification(TNotification notification)
        {
            notifications.Add(notification);
        }

        public IEnumerable<TNotification> TakeMany()
        {
            return notifications.GetConsumingEnumerable();
        }

        public bool IsCompleted
        {
            get { return notifications.IsCompleted; }
        }

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
            ChangeScale(ScaleSize);
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

            notifications.CompleteAdding();

            lock (lockWorkers)
            {
                // Kill all workers right away
                if (immediately)
                    workers.ForEach(sw => sw.Cancel());

                var all = (from sw in workers
                           select sw.WorkerTask).ToArray();

                LogAgent.Info("Stopping: Waiting on Tasks");

                Task.WaitAll(all);

                LogAgent.Info("Stopping: Done Waiting on Tasks");

                workers.Clear();
            }
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

        public void ChangeScale(int newScaleSize)
        {
            if (newScaleSize <= 0)
                throw new ArgumentOutOfRangeException("newScaleSize", "Must be Greater than Zero");

            ScaleSize = newScaleSize;

            if (!running)
                return;

            lock (lockWorkers)
            {

                // Scale down
                while (workers.Count > ScaleSize)
                {
                    workers[0].Cancel();
                    workers.RemoveAt(0);
                }

                // Scale up
                while (workers.Count < ScaleSize)
                {
                    var worker = new ServiceWorkerAdapter<TNotification>(this, ServiceConnectionFactory.Create());
                    workers.Add(worker);
                    worker.Start();
                }

                LogAgent.Debug("Scaled Changed to: " + workers.Count);
            }
        }

        #region 推送通知
        public void RaiseNotificationSucceeded(TNotification notification)
        {
            var evt = OnNotificationSucceeded;
            if (evt != null)
                evt(notification);
        }

        public void RaiseNotificationFailed(TNotification notification, AggregateException exception)
        {
            var evt = OnNotificationFailed;
            if (evt != null)
                evt(notification, exception);
        }

      
        #endregion
    }
}
