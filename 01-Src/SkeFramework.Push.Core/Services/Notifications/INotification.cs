using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Interfaces
{
    /// <summary>
    /// 通知接口
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// 设备注册ID是否有效
        /// </summary>
        /// <returns></returns>
        bool IsDeviceRegistrationIdValid();
        /// <summary>
        /// 设备标识
        /// </summary>
        object Tag { get; set; }
    }
}
