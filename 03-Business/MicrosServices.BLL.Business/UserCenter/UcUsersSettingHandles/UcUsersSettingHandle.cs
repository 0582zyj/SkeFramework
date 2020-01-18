using MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersSettingHandles;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.UserCenter.UcUsersSettingHandles
{
    public class UcUsersSettingHandle : UcUsersSettingHandleCommon, IUcUsersSettingHandle
    {
        public UcUsersSettingHandle(IRepository<UcUsersSetting> dataSerialer)
            : base(dataSerialer)
        {
        }


        /// <summary>
        /// 获取用户设定
        /// </summary>
        /// <param name="usersNo"></param>
        /// <returns></returns>
       public UcUsersSetting GetUcUsersSettingInfo(string UserNo)
        {
            Expression<Func<UcUsersSetting, bool>> where = (o => o.UserNo == UserNo);
            return this.Get(where);
        }

     
    }

}
