using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Form;

namespace MicrosServices.BLL.SHBusiness.PsOrganizationHandles
{
    public interface IPsOrganizationHandle : IPsOrganizationHandleCommon
    {
        /// <summary>
        /// 新增机构
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int OrganizationInsert(PsOrganization model);
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int OrganizationUpdate(PsOrganization model);
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="int"></param>
        int OrganizationDelete(int id);
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="OrgNo"></param>
        /// <returns></returns>
        bool CheckOrgNoIsExist(long OrgNo);
        /// <summary>
        /// 获取机构信息
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        PsOrganization GetOrgInfo(long OrgNo);
        /// <summary>
        /// 获取机构树列表
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        List<TreeNodeInfo> GetPlatformOrganizationTree(long platformNo);
        /// <summary>
        /// 检查编号是否有子节点
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        bool CheckNoHasChild(long OrgNo);
        /// <summary>
        /// 刷新子节点编号路径
        /// </summary>
        /// <param name="current"></param>
        void RefreshChildTreeLevelNo(PsOrganization current);
    }
}
