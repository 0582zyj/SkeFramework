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
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Form;

namespace MicrosServices.BLL.SHBusiness.PsUserRolesHandles
{
    public class PsUserRolesHandle : PsUserRolesHandleCommon, IPsUserRolesHandle
    {
        public PsUserRolesHandle(IRepository<PsUserRoles> dataSerialer)
            : base(dataSerialer)
        {
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
        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="usersNo"></param>
        /// <returns></returns>
        public RolesAssignVo GetRolesAssign(long UsersNo)
        {
            RolesAssignVo assignVo = new RolesAssignVo();
            List<PsUserRoles>  psUserRoles  = this.GetUserRoles(UsersNo);
            UcUsers users = DataHandleManager.Instance().UcUsersHandle.GetUsersInfo(UsersNo.ToString());
            assignVo.UserName = users == null ? "" : users.UserName;
            assignVo.UsersSettingInfo = DataHandleManager.Instance().UcUsersSettingHandle.GetUcUsersSettingInfo(UsersNo.ToString());
            long PlatformNo = assignVo.UsersSettingInfo == null ?ConstData.DefaultNo : assignVo.UsersSettingInfo.PlatformNo;
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

        public int UserRolesInsert(UserRolesForm model)
        {
            int result = 0;
            //删除原有权限
            DataHandleManager.Instance().PsUserRolesHandle.DeleteUserRoles(model.UserNo);
            PsUserRoles userRoles = null;
            if (model.RolesNos != null)
            {
                foreach (var nos in model.RolesNos)
                {
                    PsRoles roles = DataHandleManager.Instance().PsRolesHandle.GetRolesInfo(nos);
                    if (roles != null)
                    {
                        userRoles = new PsUserRoles()
                        {
                            RolesNo = nos,
                            UserNo =Convert.ToInt64( model.UserNo),
                            InputUser = model.InputUser,
                            InputTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        result += DataHandleManager.Instance().PsUserRolesHandle.Insert(userRoles);
                    }
                }
            }
            return result;
        }

      
    }
}
