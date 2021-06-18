using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsMenuHandleCommon : IDataTableHandle<PsMenu>
    {
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        List<OptionValue> GetOptionValues(long PlatformNo = -1);

        /// <summary>
        /// 根据账号获取菜单列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        List<PsMenu> GetManagementMenusList(List<long> ManagementNos);
        /// <summary>
        /// 获取所有子菜单列表
        /// </summary>
        List<PsMenu> GetChildMenuList(long MenuNo);
        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        bool UpdateTreeLevelNo(long MenuNo, string TreeLevelNo);
    }
}
