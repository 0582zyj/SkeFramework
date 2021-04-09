using MicrosServices.DAL.DataAccess.RealTimeSystem.RtShortMessageHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtShortMessageHandles
{
    public class RtShortMessageHandle : RtShortMessageHandleCommon, IRtShortMessageHandle
    {
        public RtShortMessageHandle(IRepository<RtShortMessage> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}
