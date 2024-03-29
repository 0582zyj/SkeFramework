using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;


namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsManagementRolesHandleCommon : IDataTableHandle<PsManagementRoles>
  {
        /// <summary>
        /// 删除角色的权限
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        bool DeleteManagementRoles(long RolesNo);

        /// <summary>
        /// 删除权限的角色
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        bool DeleteRoleManagementsByManagementNo(long ManagementNo);
        
    }
}
