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
    public interface IPsOrganizationHandleCommon : IDataTableHandle<PsOrganization>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        int BatchDelete(long[] rolesNos);
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        List<OptionValue> GetOptionValues(long PlatformNo = ConstData.DefaultNo);
        /// <summary>
        /// 获取所有子节点列表
        /// </summary>
        List<PsOrganization> GetChildOrganizationList(long OrgNo);
        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="OrgNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        bool UpdateTreeLevelNo(long OrgNo, string TreeLevelNo);

    }
}
