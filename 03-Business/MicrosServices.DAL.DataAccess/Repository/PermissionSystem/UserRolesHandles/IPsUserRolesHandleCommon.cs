using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsUserRolesHandleCommon : IDataTableHandle<PsUserRoles>
    {
        /// <summary>
        /// 删除用户角色列表
        /// </summary>
        /// <param name="managementNo"></param>
        bool DeleteUserRoles(string UserNo);
    }
}
