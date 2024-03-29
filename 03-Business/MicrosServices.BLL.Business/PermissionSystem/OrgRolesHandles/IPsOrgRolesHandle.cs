using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using System.Collections.Generic;
using MicrosServices.Entities.Common;

namespace MicrosServices.BLL.SHBusiness.PsOrgRolesHandles
{
    public interface IPsOrgRolesHandle : IPsOrgRolesHandleCommon
    {
        /// <summary>
        /// 机构角色修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int OrgRolesInsert(OrgRolesForm model);
        /// <summary>
        /// 获取机构角色列表
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        OrgAssignVo GetOrgAssign(long OrgNo);
        /// <summary>
        /// 获取机构角色关系列表
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        List<PsOrgRoles> GetOrgRoles(long OrgNo);

    }
}
