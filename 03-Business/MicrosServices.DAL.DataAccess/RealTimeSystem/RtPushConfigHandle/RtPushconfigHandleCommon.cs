using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.RealTimeSystem.RtPushConfigHandle;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class RtPushconfigHandleCommon : DataTableHandle<RtPushconfig>, IRtPushconfigHandleCommon
    {
        public RtPushconfigHandleCommon(IRepository<RtPushconfig> dataSerialer)
            : base(dataSerialer, RtPushconfig.TableName, false)
        {
        }
    }
}
