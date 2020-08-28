using MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtMessageHandles
{
    public interface IRtMessageHandle : IRtMessageHandleCommon
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        int RtMessageInsert(RtMessage model);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int RtMessageUpdate(RtMessage model);
    }
}
