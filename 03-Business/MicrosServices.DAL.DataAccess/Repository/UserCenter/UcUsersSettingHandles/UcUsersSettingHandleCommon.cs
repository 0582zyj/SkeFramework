using MicrosServices.Entities.Common;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersSettingHandles
{
    public class UcUsersSettingHandleCommon : DataTableHandle<UcUsersSetting>, IUcUsersSettingHandleCommon
    {
        public UcUsersSettingHandleCommon(IRepository<UcUsersSetting> dataSerialer)
            : base(dataSerialer, UcUsersSetting.TableName, false)
        {
        }
    }

}
