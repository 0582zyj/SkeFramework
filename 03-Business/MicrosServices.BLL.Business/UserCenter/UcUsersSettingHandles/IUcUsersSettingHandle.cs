using MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersSettingHandles;
using MicrosServices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.UserCenter.UcUsersSettingHandles
{
    public interface IUcUsersSettingHandle : IUcUsersSettingHandleCommon
    {
        /// <summary>
        /// 获取用户设定
        /// </summary>
        /// <param name="usersNo"></param>
        /// <returns></returns>
        UcUsersSetting GetUcUsersSettingInfo(long usersNo);
    }

}
