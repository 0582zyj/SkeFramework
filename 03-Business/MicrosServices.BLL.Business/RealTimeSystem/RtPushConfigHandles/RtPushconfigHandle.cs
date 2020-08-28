using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtPushConfigHandles
{
    public class RtPushconfigHandle : RtPushconfigHandleCommon, IRtPushconfigHandle
    {
        public RtPushconfigHandle(IRepository<RtPushconfig> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}
