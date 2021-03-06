using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsUserOrgHandleCommon : IDataTableHandle<PsUserOrg>
    {
        
        /// <summary>
        /// 删除用户机构列表
        /// </summary>
        /// <param name="managementNo"></param>
        bool DeleteUserOrgs(string UserNo);
    }
}
