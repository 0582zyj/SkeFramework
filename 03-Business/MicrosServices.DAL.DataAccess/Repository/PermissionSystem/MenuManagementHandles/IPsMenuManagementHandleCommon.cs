using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsMenuManagementHandleCommon : IDataTableHandle<PsMenuManagement>
  {
        /// <summary>
        /// 删除权限菜单列表
        /// </summary>
        /// <param name="managementNo"></param>
        bool DeleteManagementMenus(long managementNo);
    }
}
