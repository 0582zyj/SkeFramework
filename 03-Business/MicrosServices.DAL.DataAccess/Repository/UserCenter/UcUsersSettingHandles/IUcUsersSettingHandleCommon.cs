using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersSettingHandles
{
    public interface IUcUsersSettingHandleCommon : IDataTableHandle<UcUsersSetting>
    {
        /// <summary>
        /// 删除用户设定
        /// </summary>
        /// <param name="usersNo"></param>
        /// <returns></returns>
        int DeleteUserSetting(string usersNo);
    }
}
