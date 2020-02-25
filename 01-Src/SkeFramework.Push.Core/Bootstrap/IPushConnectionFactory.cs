using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Bootstrap
{
    /// <summary>
    /// 推送链接工厂
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IPushConnectionFactory<TNotification> where TNotification : INotification
    {
        /// <summary>
        /// 生成一个推送链接
        /// </summary>
        /// <returns></returns>
        IPushConnection<TNotification> Create();
    }
}
