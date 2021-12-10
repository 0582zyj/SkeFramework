using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Constants;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Bootstrap
{
    /// <summary>
    /// 引导程序抽象基类
    /// </summary>
    public abstract class AbstractBootstrap
    {
        /// <summary>
        /// 数据工厂实例延迟绑定，由应用层进行操作
        /// </summary>
        private static IPushServerFactory<INotification> _staticInstance = null;

        public static void SetDataHandleFactory(IPushServerFactory<INotification> factory)
        {
            _staticInstance = factory;
        }

        /// <summary>
        /// 初始工作线程
        /// </summary>
        protected int Workers;
        /// <summary>
        /// 连接配置
        /// </summary>
        protected IConnectionConfig config;

        #region 引导程序参数设置
        /// <summary>
        /// 设置服务端启用参数配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public AbstractBootstrap SetConfig(IConnectionConfig config)
        {
            this.config = config;
            return this;
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        public IConnectionConfig GetConfig()
        {
            return this.config;
        }
        /// <summary>
        /// 设置初始工作线程
        /// </summary>
        /// <param name="workerThreadCount"></param>
        /// <returns></returns>
        public AbstractBootstrap WorkerThreads(int workerThreadCount)
        {
            if (workerThreadCount < 1) throw new ArgumentException("Can't be below 1", "workerThreadCount");
            Workers = workerThreadCount;
            config.SetOption(DefaultConfigTypeEumns.Workers.ToString(), Workers);
            return this;
        }
        #endregion

        #region 创建服务端工厂     
        /// <summary>
        /// 数据访问统一入口
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="controlerManager"></param>
        /// <returns></returns>
        public THandle BuildPushBroker<THandle, TData>()
            where THandle : class, IPushBroker<TData>
            where TData : INotification, new()
        {
            Validate();
            if (_staticInstance != null)
            {
              return  _staticInstance.NewPushBroker(this.config) as THandle;
            }
            return BuildPushServerFactory<TData>().NewPushBroker(this.config) as THandle;
        }

        protected bool IsSubclassOf(Type parentType, Type childType)
        {
            return parentType == childType || parentType.IsSubclassOf(childType);
        }
        /// <summary>
        /// 检查抽象接口
        /// </summary>
        public abstract void Validate();
        /// <summary>
        /// 数据工厂生成接口
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public abstract IPushServerFactory<TData> BuildPushServerFactory<TData>() where TData : INotification, new();
        #endregion
    }
}
