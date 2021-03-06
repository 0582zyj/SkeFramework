using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core;
using System.Collections.Generic;
using MicrosServices.Entities.Common;

namespace MicrosServices.BLL.SHBusiness.PsManagementRolesHandles
{
    public interface IPsManagementRolesHandle : IPsManagementRolesHandleCommon
    {
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int ManagementRolesInsert(ManagementRolesForm model);
        /// <summary>
        /// 检查角色权限是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckManagementRolesNoIsExist(long ManagementNo, long RolesNo);
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        ManagmentAssignVo GetManagementAssign(long RolesNo, long ManagementType);
        /// <summary>
        /// 获取权限关系列表
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        List<PsManagementRoles> GetManagementRoles(long RolesNo);
      
    }
}
