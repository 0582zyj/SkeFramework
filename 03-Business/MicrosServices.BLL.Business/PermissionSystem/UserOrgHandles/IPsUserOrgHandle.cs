using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.VO;
using MicrosServices.Helper.Core.Form;
using System.Collections.Generic;
using MicrosServices.Entities.Common;

namespace MicrosServices.BLL.SHBusiness.PsUserOrgHandles
{
    public interface IPsUserOrgHandle : IPsUserOrgHandleCommon
    {
        /// <summary>
        /// 获取用户机构信息
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        UserOrgAssignVo GetUserOrgAssign(long UserNo);
        /// <summary>
        /// 新增用户机构信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UserOrgsInsert(UserOrgsForm model);
        /// <summary>
        /// 获取用户机构列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        List<PsUserOrg> GetUserOrgs(long UserNo);
        /// <summary>
        /// 检查用户机构是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckUserOrgNoIsExist(long UserNo, long OrgNo);
    }
}
