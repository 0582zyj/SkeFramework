using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles
{
    /// <summary>
    /// 推送记录数据库访问实现
    /// </summary>
    public class RtMessageHandleCommon : DataTableHandle<RtMessage>, IRtMessageHandleCommon
    {
        public RtMessageHandleCommon(IRepository<RtMessage> dataSerialer)
            : base(dataSerialer, RtMessage.TableName, false)
        {
        }
    }
}
