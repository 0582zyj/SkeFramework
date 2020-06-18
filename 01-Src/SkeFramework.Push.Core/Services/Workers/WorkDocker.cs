using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Services.Workers
{
    /// <summary>
    /// 工作线程管理
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public class WorkDocker<TNotification> where TNotification : INotification
    {
        private readonly IPushBroker<TNotification> PushBroker;
        private readonly IPushConnectionFactory ConnectionFactory;
        /// <summary>
        /// 推送工作线程数量
        /// </summary>
        public int ScaleSize { get; private set; }
        /// <summary>
        /// 推送线程集合
        /// </summary>
        List<ServiceWorkerAdapter<TNotification>> workers;
        /// <summary>
        /// 线程安全锁
        /// </summary>
        private object lockWorkers;

        public WorkDocker(IPushBroker<TNotification> broker, IPushConnectionFactory connectionFactory)
        {
            PushBroker = broker;
            ConnectionFactory = connectionFactory;
            lockWorkers = new object();
            workers = new List<ServiceWorkerAdapter<TNotification>>();
            ScaleSize = 1;
            //AutoScale = true;
            //AutoScaleMaxSize = 20;
        }
       
        #region 启动和关闭工作进程
        /// <summary>
        /// 改变推送进程数量
        /// </summary>
        /// <param name="newScaleSize"></param>
        public void ChangeScale(int newScaleSize)
        {
            if (newScaleSize <= 0)
                throw new ArgumentOutOfRangeException("newScaleSize", "Must be Greater than Zero");

            ScaleSize = newScaleSize;

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
                    var worker = new ServiceWorkerAdapter<TNotification>(PushBroker, ConnectionFactory.Create());
                    workers.Add(worker);
                    worker.Start();
                }

                LogAgent.Debug("Scaled Changed to: " + workers.Count);
            }
        }
        /// <summary>
        /// 停止工作进程
        /// </summary>
        /// <param name="immediately"></param>
        public void StopWorker(bool immediately = false)
        {
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
        }

        #endregion

        /// <summary>
        /// 获取未使用的进程
        /// </summary>
        /// <returns></returns>
        public ServiceWorkerAdapter<TNotification> GetServiceWorkerAdapter()
        {
            ServiceWorkerAdapter<TNotification> serviceWorker= workers.Find(o => o.IsCompleted == true);
            if (serviceWorker != null)
                return serviceWorker;
            return workers.FirstOrDefault();
        }

    }
}
