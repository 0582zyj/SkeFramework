using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtShortMessageHandles
{
    public class RtShortMessageHandleCommon : DataTableHandle<RtShortMessage>, IRtShortMessageHandleCommon
    {
        public RtShortMessageHandleCommon(IRepository<RtShortMessage> dataSerialer)
            : base(dataSerialer, RtShortMessage.TableName, false)
        {
        }
    }
}
