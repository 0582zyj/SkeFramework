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
    /// 推送链接工厂
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IPushConnectionFactory
    {
        /// <summary>
        /// 生成一个推送链接
        /// </summary>
        /// <returns></returns>
        IPushConnection<INotification> Create();
    }
}
