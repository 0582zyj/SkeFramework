using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common.RealTimeSystem;

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtEmailHandles
{
    public class RtEmailHandleCommon : DataTableHandle<RtEmail>, IRtEmailHandleCommon
    {
        public RtEmailHandleCommon(IRepository<RtEmail> dataSerialer)
            : base(dataSerialer, RtEmail.TableName, false)
        {
        }
    }
}
