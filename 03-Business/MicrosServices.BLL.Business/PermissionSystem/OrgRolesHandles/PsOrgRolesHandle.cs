using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.Form;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.VO;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Extends;
using MicrosServices.Helper.Core.Common;
using System.Linq.Expressions;

namespace MicrosServices.BLL.SHBusiness.PsOrgRolesHandles
{
    public class PsOrgRolesHandle : PsOrgRolesHandleCommon, IPsOrgRolesHandle
    {
        public PsOrgRolesHandle(IRepository<PsOrgRoles> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 机构角色修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrgRolesInsert(OrgRolesForm model)
        {
            int result = 0;
            //删除原有权限
            DataHandleManager.Instance().PsOrgRolesHandle.DeleteOrgRoles(model.OrgNo);
            PsOrgRoles orgRoles = null;
            if (model.RolesNos != null)
            {
                foreach (var nos in model.RolesNos)
                {
                    PsRoles roles = DataHandleManager.Instance().PsRolesHandle.GetRolesInfo(nos);
                    if (roles != null)
                    {
                        orgRoles = new PsOrgRoles()
                        {
                            RolesNo = nos,
                            OrgNo = model.OrgNo,
                            InputUser = model.InputUser,
                            InputTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        result += DataHandleManager.Instance().PsOrgRolesHandle.Insert(orgRoles);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取机构角色列表
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public OrgAssignVo GetOrgAssign(long OrgNo)
        {
            OrgAssignVo assignVo = new OrgAssignVo();
            List<PsOrgRoles>  orgRoles  = this.GetOrgRoles(OrgNo);
            assignVo.OrgInfo = DataHandleManager.Instance().PsOrganizationHandle.GetOrgInfo(OrgNo);
            List<OptionValue> optionValues = DataHandleManager.Instance().PsRolesHandle.GetOptionValues(assignVo.OrgInfo.PlatformNo);
            assignVo.optionValues = new List<CheckOptionValue>();
            foreach (var item in optionValues)
            {
                bool isCheck = orgRoles.Where(o => o.RolesNo == item.Value).FirstOrDefault() != null;
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
        /// 获取机构角色关系列表
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public List<PsOrgRoles> GetOrgRoles(long OrgNo)
        {
            Expression<Func<PsOrgRoles, bool>> where = (o => o.OrgNo == OrgNo);
            return this.GetList(where).ToList();
        }
      

    }
}
