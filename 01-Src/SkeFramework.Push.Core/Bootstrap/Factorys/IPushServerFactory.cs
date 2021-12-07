using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Bootstrap
{
    /// <summary>
    /// 推送服务端
    /// </summary>
    public interface IPushServerFactory<TNotification> : IPushConnectionFactory<TNotification> where TNotification:INotification
    {
        /// <summary>
        /// 对象生成器
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        IPushBroker<TNotification> NewPushBroker(IConnectionConfig connectionConfig);
    }
}
