using MicrosServices.DAL.DataAccess.RealTimeSystem.RtPushConfigHandle;
using MicrosServices.Entities.Common.RealTimeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtPushConfigHandles
{
    public interface IRtPushconfigHandle : IRtPushconfigHandleCommon
    {
        /// <summary>
        /// 新增推送服务端配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int PushconfigInsert(RtPushconfig model);
        /// <summary>
        /// 更新推送服务端配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int PushconfigUpdate(RtPushconfig model);
    }
}
