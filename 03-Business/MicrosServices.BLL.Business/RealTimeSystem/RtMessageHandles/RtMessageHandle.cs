using MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtMessageHandles
{
    public class RtMessageHandle : RtMessageHandleCommon, IRtMessageHandle
    {
        public RtMessageHandle(IRepository<RtMessage> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}
