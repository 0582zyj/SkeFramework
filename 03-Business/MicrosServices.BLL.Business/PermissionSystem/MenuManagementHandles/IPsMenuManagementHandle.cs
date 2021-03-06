using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using MicrosServices.Entities.Common;
using System.Collections.Generic;
using MicrosServices.Helper.Core.VO.AssignVo;
using MicrosServices.Helper.Core.Form.AssignForm;
using MicrosServices.Helper.Core.Extends;

namespace MicrosServices.BLL.SHBusiness.PsMenuRolesHandles
{
    public interface IPsMenuManagementHandle : IPsMenuManagementHandleCommon
    {
        /// <summary>
        /// 权限菜单授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int ManagementMenusInsert(ManagementMenusForm model);
        /// <summary>
        /// 检查权限菜单是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckManagementMenusNoIsExist(long MenuNo, long ManagementNo);
        /// <summary>
        /// 获取权限菜单列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        MenuAssignVo GetMenuAssign(long ManagementNo);      
        /// <summary>
        /// 获取权限菜单关系列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        List<PsMenuManagement> GetManagementMenus(long ManagementNo);
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="menuNo"></param>
        /// <returns></returns>
        MenuManagmentAssignVo GetMenuManagmentAssignVo(long menuNo);
        /// <summary>
        /// 获取菜单权限关系列表
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        List<PsMenuManagement> GetMenuManagements(long MenuNo);
        /// <summary>
        /// 菜单权限授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int MenuManagementsInsert(MenuManagementsForm model);
    
    }
}
