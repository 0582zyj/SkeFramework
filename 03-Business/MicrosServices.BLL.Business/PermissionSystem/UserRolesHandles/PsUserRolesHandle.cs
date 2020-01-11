using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.VO;
using System.Collections.Generic;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using System.Linq.Expressions;

namespace MicrosServices.BLL.SHBusiness.PsUserRolesHandles
{
    public class PsUserRolesHandle : PsUserRolesHandleCommon, IPsUserRolesHandle
    {
        public PsUserRolesHandle(IRepository<PsUserRoles> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="usersNo"></param>
        /// <returns></returns>
        public RolesAssignVo GetRolesAssign(long UsersNo)
        {
            RolesAssignVo assignVo = new RolesAssignVo();
            List<PsUserRoles>  psUserRoles  = this.GetUserRoles(UsersNo);
            assignVo.UsersInfo = DataHandleManager.Instance().UcUsersHandle.GetUsersInfo(UsersNo.ToString());
            UcUsersSetting usersSetting = DataHandleManager.Instance().UcUsersSettingHandle.GetUcUsersSettingInfo(UsersNo);
            long PlatformNo = 0;
            List<OptionValue> optionValues = DataHandleManager.Instance().PsRolesHandle.GetOptionValues(PlatformNo);
            assignVo.optionValues = new List<CheckOptionValue>();
            foreach (var item in optionValues)
            {
                bool isCheck = psUserRoles.Where(o => o.RolesNo == item.Value).FirstOrDefault() != null;
                assignVo.optionValues.Add(new CheckOptionValue()
                {
                    isCheck = isCheck,
                    Name = item.Name,
                    Value = item.Value
                });
            }
            return assignVo;
        }
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="UsersNo"></param>
        /// <returns></returns>
        private List<PsUserRoles> GetUserRoles(long UsersNo)
        {
            Expression<Func<PsUserRoles, bool>> where = (o => o.UserNo == UsersNo);
            return this.GetList(where).ToList();
        }
    }
}
