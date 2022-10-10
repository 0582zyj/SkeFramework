using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Constants;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            ServiceWorkerAdapter<TNotification> serviceWorker= workers.Find(o => this.MatchSubprotocol(o.Connection, tag)); 
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
            ServiceWorkerAdapter<TNotification> serviceWorker = workers.Find(o =>this.MatchSubprotocol(o.Connection,taskId));
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

        /// <summary>
        /// 链接标识匹配
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        protected virtual bool MatchSubprotocol(IPushConnection<TNotification> connection,string taskId)
        {
            if (connection == null && connection.GetTag().Length == 0)
            {
                return false;
            }
            string requestTag = connection.GetTag();
            bool isMatch = requestTag.Equals(taskId);
            if (isMatch)
                return true;
            if (requestTag.Contains("+") || requestTag.Contains("*"))
            {
                string[] requestedSubprotocolArray = requestTag.Split('/');
                string[] responseSubprotocolArray = taskId.Split('/');
                if (requestedSubprotocolArray.Length != responseSubprotocolArray.Length)
                    return false;
                int var4 = requestedSubprotocolArray.Length;
                int var9 = responseSubprotocolArray.Length;
                for (int var5 = 0; var5 < var4; ++var5)
                {
                    string requestedSubprotocol = requestedSubprotocolArray[var5];
                    string supportedSubprotocol = responseSubprotocolArray[var5];
                    if (!("+".Equals(requestedSubprotocol) || "*".Equals(requestedSubprotocol) || requestedSubprotocol.Equals(supportedSubprotocol)))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
