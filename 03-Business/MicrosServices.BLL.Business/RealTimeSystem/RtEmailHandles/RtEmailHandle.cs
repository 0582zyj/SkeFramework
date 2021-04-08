using MicrosServices.DAL.DataAccess.RealTimeSystem.RtEmailHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtEmailHandles
{
    public class RtEmailHandle : RtEmailHandleCommon, IRtEmailHandle
    {
        public RtEmailHandle(IRepository<RtEmail> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}
