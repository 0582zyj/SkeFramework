using MicrosServices.Entities.Common.UserCenter;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersInfoHandles
{
    public class UcUsersInfoHandleCommon : DataTableHandle<UcUsersInfo>, IUcUsersInfoHandleCommon
    {
        public UcUsersInfoHandleCommon(IRepository<UcUsersInfo> dataSerialer)
            : base(dataSerialer, UcUsersInfo.TableName, false)
        {
        }
    }
}
