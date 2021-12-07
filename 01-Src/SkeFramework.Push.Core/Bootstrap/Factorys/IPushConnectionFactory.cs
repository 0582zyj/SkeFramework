using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Bootstrap
{
    /// <summary>
    /// 推送链接
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IPushConnectionFactory<TNotification> where TNotification : INotification
    {
        /// <summary>
        /// 生成一个推送链接
        /// </summary>
        /// <returns></returns>
        IPushConnection<TNotification> Create(IConnectionConfig config=null);
        /// <summary>
        /// 关联推送反应堆
        /// </summary>
        /// <param name="pushBroker"></param>
        void SetRelatedPushBroker(IPushBroker<TNotification> pushBroker);
    }
}
