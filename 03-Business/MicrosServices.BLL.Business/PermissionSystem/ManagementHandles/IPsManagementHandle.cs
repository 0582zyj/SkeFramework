using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using System.Collections.Generic;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;

namespace MicrosServices.BLL.SHBusiness.PsManagementHandles
{
    public interface IPsManagementHandle : IPsManagementHandleCommon
    {
        /// <summary>
        /// 新增一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int ManagementInsert(PsManagement management);
        /// <summary>
        /// 更新一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int ManagementUpdate(PsManagement management);
        /// <summary>
        /// 删除一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int ManagementDelete(int id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int ManagementBeachDelete(List<long> ManagementNos);
        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        bool CheckManagementNameIsExist(string Name, long ManagementNo = ConstData.DefaultNo);       
        /// <summary>
        /// 检查编码是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckManagementNoIsExist(long ManagementNo);
        /// <summary>
        /// 检查编码是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckManagementNosIsExist(List<long> ManagementNos);
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        PsManagement GetManagementInfo(long ManagementNo);
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        List<OptionValue> GetRolesOptionValues(long PlatformNo, int ManagementType = ConstData.DefaultInt);
        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        List<TreeNodeInfo> GetPlatformManagementTree(long platformNo);

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>      
        List<ManagementOptionValue> GetUserManagementList(string UserNo);
        /// <summary>
        /// 获取用户分组列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        List<PsManagement> GetUserManagementGroupList(string UserNo);
        /// <summary>
        /// 检查编号是否有子节点
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckNoHasChild(long ManagementNo);
        /// <summary>
        /// 刷新子节点编号路径
        /// </summary>
        /// <param name="current"></param>
        void RefreshChildTreeLevelNo(PsManagement current);
        /// <summary>
        /// 统计平台的权限数
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        long CountByPlatformNo(long PlatformNo);
    }
}
