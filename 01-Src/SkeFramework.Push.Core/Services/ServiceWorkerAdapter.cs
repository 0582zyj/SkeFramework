using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Services
{
    /// <summary>
    /// 推送工作线程
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public class ServiceWorkerAdapter<TNotification> where TNotification : INotification
    {

        public ServiceWorkerAdapter(IPushBroker<TNotification> broker, IPushConnection<TNotification> connection)
        {
            Broker = broker;
            Connection = connection;
            CancelTokenSource = new CancellationTokenSource();
            notifications = new BlockingCollection<TNotification>();
        }
        /// <summary>
        /// 推送服务器接口
        /// </summary>
        public IPushBroker<TNotification> Broker { get; private set; }
        /// <summary>
        /// 推送链接
        /// </summary>
        public IPushConnection<TNotification> Connection  { get; private set; }
        #region 开始和关闭推送线程
        /// <summary>
        /// 取消发送【多线程】
        /// </summary>
        public CancellationTokenSource CancelTokenSource { get; private set; }
        /// <summary>
        /// 工作任务
        /// </summary>
        public Task WorkerTask { get; private set; }
        /// <summary>
        /// 开始任务
        /// </summary>
        public void Start()
        {
            WorkerTask = Task.Factory.StartNew(async delegate {
                while (!CancelTokenSource.IsCancellationRequested && !this.IsCompleted)
                {
                    try
                    {
                        //发送任务列表
                        var toSend = new List<Task>();
                        foreach (var n in this.TakeMany())
                        {
                            var t = Connection.Send(n);
                            // Keep the continuation
                            var cont = t.ContinueWith(ct => {
                                var cn = n;
                                var ex = t.Exception;
                                if (ex == null)
                                    Broker.RaiseNotificationSucceeded(cn);
                                else
                                    Broker.RaiseNotificationFailed(cn, ex);
                            });
                            // Let's wait for the continuation not the task itself
                            toSend.Add(cont);
                        }

                        if (toSend.Count <= 0)
                            continue;
                        try
                        {
                            LogAgent.Info("Waiting on all tasks {0}", toSend.Count());
                            await Task.WhenAll(toSend).ConfigureAwait(false);
                            LogAgent.Info("All Tasks Finished");
                        }
                        catch (Exception ex)
                        {
                            LogAgent.Error("Waiting on all tasks Failed: {0}", ex);
                        }
                        LogAgent.Info("Passed WhenAll");
                    }
                    catch (Exception ex)
                    {
                        LogAgent.Error("Broker.Take: {0}", ex);
                    }
                }

                if (CancelTokenSource.IsCancellationRequested)
                    LogAgent.Info("Cancellation was requested");
                if (this.IsCompleted)
                    LogAgent.Info("Broker IsCompleted");
                LogAgent.Debug("Broker Task Ended");

            }, CancelTokenSource.Token, TaskCreationOptions.LongRunning
            | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Unwrap();

            //工作任务异常的延续任务
            WorkerTask.ContinueWith(t => {
                var ex = t.Exception;
                if (ex != null)
                    LogAgent.Error("ServiceWorkerAdapter.WorkerTask Error: {0}", ex);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
        /// <summary>
        /// 取消任务
        /// </summary>
        public void Cancel()
        {
            notifications.CompleteAdding();
            CancelTokenSource.Cancel();
        }
        #endregion

        #region 通知任务队列
        /// <summary>
        /// 通知消息列表
        /// </summary>
        BlockingCollection<TNotification> notifications;
        /// <summary>
        /// 新增一个通知
        /// </summary>
        /// <param name="notification"></param>
        public virtual void QueueNotification(TNotification notification)
        {
            notifications.Add(notification);
        }
        /// <summary>
        /// 获取可消费的通知列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TNotification> TakeMany()
        {
            return notifications.GetConsumingEnumerable();
        }
        /// <summary>
        /// 判断生产者线程是否已经完成添加并且没有元素被消费
        /// </summary>
        public bool IsCompleted
        {
            get { return notifications.IsCompleted; }
        }
        #endregion
    }
}