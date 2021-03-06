using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using System.Collections.Generic;

namespace MicrosServices.BLL.SHBusiness.PsRolesHandles
{
    public interface IPsRolesHandle : IPsRolesHandleCommon
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int RolesInsert(PsRoles model);
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int RolesUpdate(PsRoles model);
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        bool CheckRolesNoIsExist(long Nos);
        /// <summary>
        /// 根据角色编号获取信息
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
       PsRoles GetRolesInfo(long RolesNo);
     
    }
}
