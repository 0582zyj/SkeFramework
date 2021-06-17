using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Extends;
using System.Collections.Generic;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    /// <summary>
    /// 菜单-权限-关系表
    /// </summary>
    public interface IPsMenuManagementHandleCommon : IDataTableHandle<PsMenuManagement>
    {
        /// <summary>
        /// 删除权限菜单列表
        /// </summary>
        /// <param name="managementNo"></param>
        bool DeleteManagementMenus(long managementNo, List<int> TypeList);
        /// <summary>
        /// 删除菜单权限列表
        /// </summary>
        /// <param name="managementNo"></param>
        bool DeleteMenuManagements(long MenuNo,List<int> TypeList);
        /// <summary>
        /// 获取菜单权限操作列表
        /// </summary>
        /// <returns></returns>
        List<ManagementOptionValue> GetManagementOptionValues(List<long> MenuNos, int ManagementType);
    }
}
