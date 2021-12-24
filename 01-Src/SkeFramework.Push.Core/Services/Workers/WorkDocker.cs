using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Constants;
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
        /// <summary>
        /// 线程安全锁
        /// </summary>
        private object lockWorkers;
        /// <summary>
        /// 推送核心
        /// </summary>
        protected readonly IPushBroker<TNotification> PushBroker;
        /// <summary>
        /// 推送链接
        /// </summary>
        protected readonly IPushConnectionFactory<TNotification> ConnectionFactory;
        /// <summary>
        /// 推送线程集合
        /// </summary>
        protected List<ServiceWorkerAdapter<TNotification>> workers;
        /// <summary>
        /// 推送工作线程数量
        /// </summary>
        public int ScaleSize { get; private set; }
        /// <summary>
        /// 默认链接处理标识
        /// </summary>
        private readonly IConnectionConfig defaultConfig;

        public WorkDocker(IPushBroker<TNotification> broker, IPushConnectionFactory<TNotification> connectionFactory, IConnectionConfig defaultConfig)
        {
            PushBroker = broker;
            ConnectionFactory = connectionFactory;
            this.defaultConfig = defaultConfig;
            lockWorkers = new object();
            workers = new List<ServiceWorkerAdapter<TNotification>>();
            ScaleSize = 0;
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
                    ServiceWorkerAdapter<TNotification>  serviceWorker= AddServiceWorkerAdapter(defaultConfig);
                    if (serviceWorker == null)
                    {
                        break;
                    }
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
            if (defaultConfig==null||!defaultConfig.HasOption(DefaultConfigTypeEumns.ConnectionTag.ToString()))
            {
                return null;
            }
            string tag = defaultConfig.GetOption(DefaultConfigTypeEumns.ConnectionTag.ToString()).ToString();
            ServiceWorkerAdapter<TNotification> serviceWorker= workers.Find(o => o.Connection != null && o.Connection.GetTag().Equals(tag)); ;
            if (serviceWorker != null)
                return serviceWorker;
            return workers.FirstOrDefault();
        }

        /// <summary>
        /// 获取未使用的进程
        /// </summary>
        /// <returns></returns>
        public ServiceWorkerAdapter<TNotification> GetServiceWorkerAdapter(string taskId)
        {
            ServiceWorkerAdapter<TNotification> serviceWorker = workers.Find(o => o.Connection!=null&& o.Connection.GetTag().Equals(taskId));
            if (serviceWorker != null)
                return serviceWorker;
            return null ;
        }
        /// <summary>
        /// 新增进程
        /// </summary>
        /// <param name="config"></param>
        public virtual ServiceWorkerAdapter<TNotification> AddServiceWorkerAdapter(IConnectionConfig config)
        {
            IPushConnection<TNotification> connection = ConnectionFactory.Create(config);
            if (connection == null)
            {
                return null;
            }
            var worker = new ServiceWorkerAdapter<TNotification>(PushBroker, connection);
            string ResultDataKey = DefaultConfigTypeEumns.ResultData.ToString();
            if (config.HasOption(ResultDataKey))
            {
                TNotification notification = config.GetOption<TNotification>(ResultDataKey);
                worker.QueueNotification(notification);
            }
            workers.Add(worker);
            worker.Start();
            LogAgent.Debug("新增工作线程：" + worker.Connection.GetTag());
            return worker;
        }

    }
}
