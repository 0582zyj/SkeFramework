using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using System.Collections.Generic;
using MicrosServices.Entities.Constants;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsRolesHandleCommon : IDataTableHandle<PsRoles>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        int BatchDelete(long[] Nos);
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        List<OptionValue> GetOptionValues(long PlatformNo = ConstData.DefaultNo);
        /// <summary>
        /// 获取获取用户角色列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        List<PsRoles> GetUserRoleList(string UserNo);
        /// <summary>
        /// 获取所有子节点列表
        /// </summary>
        List<PsRoles> GetChildManagementList(long ManagementNo);
        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        bool UpdateTreeLevelNo(long ManagementNo, string TreeLevelNo);
    }
}
