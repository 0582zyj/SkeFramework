using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.Form;

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
        /// 删除角色的权限
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        bool DeleteManagementRoles(long RolesNo);
    }
}
