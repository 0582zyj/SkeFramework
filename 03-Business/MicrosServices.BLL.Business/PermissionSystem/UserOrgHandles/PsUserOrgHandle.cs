using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.VO;
using System.Collections.Generic;
using System.Linq.Expressions;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Form;

namespace MicrosServices.BLL.SHBusiness.PsUserOrgHandles
{
    public class PsUserOrgHandle : PsUserOrgHandleCommon, IPsUserOrgHandle
    {
        public PsUserOrgHandle(IRepository<PsUserOrg> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 权限菜单授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UserOrgsInsert(UserOrgsForm model)
        {
            int result = 0;
            //删除原有权限
            DataHandleManager.Instance().PsUserOrgHandle.DeleteUserOrgs(model.userNo);
            PsUserOrg userOrg = null;
            if (model.orgNos != null)
            {
                model.orgNos = model.orgNos.Distinct().ToArray();
                foreach (var nos in model.orgNos)
                {
                    PsOrganization organization = DataHandleManager.Instance().PsOrganizationHandle.GetOrgInfo(nos);
                    if (organization != null)
                    {
                        userOrg = new PsUserOrg()
                        {
                            OrgNo = nos,
                            UserNo =Convert.ToInt64( model.userNo),
                            InputUser = model.inputUser,
                            InputTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        result += DataHandleManager.Instance().PsUserOrgHandle.Insert(userOrg);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 检查用户机构是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckUserOrgNoIsExist(long UserNo, long OrgNo)
        {
            if (OrgNo != ConstData.DefaultNo)
            {
                Expression<Func<PsUserOrg, bool>> where = (o => o.UserNo == UserNo && o.OrgNo == OrgNo);
                long count = this.Count(where);
                if (count == 0)
                {
                    throw new ArgumentException(String.Format("用户机构[{0}-{1}]已存在", UserNo, OrgNo));
                }
            }
            return false;
        }
        /// <summary>
        /// 获取用户机构列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public UserOrgAssignVo GetUserOrgAssign(long UserNo)
        {
            UserOrgAssignVo assignVo = new UserOrgAssignVo();
            List<PsUserOrg> userOrgs = this.GetUserOrgs(UserNo);
            UcUsers users = DataHandleManager.Instance().UcUsersHandle.GetUsersInfo(UserNo.ToString());
            assignVo.userName = users == null ? "" : users.UserName;
            assignVo.userNo = users == null ? -1 : long.Parse( users.UserNo); 
            assignVo.UsersSettingInfo = DataHandleManager.Instance().UcUsersSettingHandle.GetUcUsersSettingInfo(UserNo.ToString());
            long PlatformNo = assignVo.UsersSettingInfo == null ? ConstData.DefaultNo : assignVo.UsersSettingInfo.PlatformNo;
            List<OptionValue> optionValues = DataHandleManager.Instance().PsOrganizationHandle.GetOptionValues(new List<long>() { PlatformNo });
            assignVo.optionValues = new List<CheckOptionValue>();
            foreach (var item in optionValues)
            {
                bool isCheck = userOrgs.Where(o => o.OrgNo == item.Value).FirstOrDefault() != null;
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
        /// 获取用户机构列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public List<PsUserOrg> GetUserOrgs(long UserNo)
        {
            Expression<Func<PsUserOrg, bool>> where = (o => o.UserNo == UserNo);
            return this.GetList(where).ToList();
        }
    }
}
