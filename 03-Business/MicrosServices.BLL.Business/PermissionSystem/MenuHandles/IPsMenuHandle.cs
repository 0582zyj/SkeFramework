using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using System.Collections.Generic;

namespace MicrosServices.BLL.SHBusiness.PsMenuHandles
{
    public interface IPsMenuHandle : IPsMenuHandleCommon
    {
        /// <summary>
        /// 检查名称是否重复
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        bool CheckNameIsExist(long MenuNo, string Name);
        /// <summary>
        /// 检查菜单编号是否存在
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        bool CheckMenuNoIsExist(long MenuNo);
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int MenuInsert(PsMenu model);
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int MenuUpdate(PsMenu management);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        List<OptionValue> GetMenusOptionValues(long platformNo);
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        PsMenu GetMenuInfo(long MenuNo);
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        List<PsMenu> GetUserMenusList(string UserNo);
        /// <summary>
        /// 获取平台菜单树
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        List<TreeNodeInfo> GetPlatformMenuTree(long platformNo);
    }
}
