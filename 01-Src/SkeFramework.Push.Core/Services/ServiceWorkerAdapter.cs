using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using System;
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
        }
        /// <summary>
        /// 推送接口
        /// </summary>
        public IPushBroker<TNotification> Broker { get; private set; }
        /// <summary>
        /// 推送链接
        /// </summary>
        public IPushConnection<TNotification> Connection { get; private set; }
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
                while (!CancelTokenSource.IsCancellationRequested && !Broker.IsCompleted)
                {
                    try
                    {
                        var toSend = new List<Task>();
                        foreach (var n in Broker.TakeMany())
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
                if (Broker.IsCompleted)
                    LogAgent.Info("Broker IsCompleted");

                LogAgent.Debug("Broker Task Ended");
            }, CancelTokenSource.Token, TaskCreationOptions.LongRunning
            | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Unwrap();

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
            CancelTokenSource.Cancel();
        }
    }
}