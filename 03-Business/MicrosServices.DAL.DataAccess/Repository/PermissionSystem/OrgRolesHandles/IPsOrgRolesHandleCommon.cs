using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsOrgRolesHandleCommon : IDataTableHandle<PsOrgRoles>
  {
        /// <summary>
        /// 删除机构角色
        /// </summary>
        /// <param name="OrgNo"></param>
        /// <returns></returns>
        bool DeleteOrgRoles(long OrgNo);
    }
}
