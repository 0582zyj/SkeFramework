using MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersInfoHandles;
using MicrosServices.Entities.Common.UserCenter;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.UserCenter.UcUsersInfoHandles
{
    public class UcUsersInfoHandle : UcUsersInfoHandleCommon, IUcUsersInfoHandle
    {
        public UcUsersInfoHandle(IRepository<UcUsersInfo> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}
