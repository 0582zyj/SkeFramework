using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using System.Collections.Generic;

namespace MicrosServices.BLL.SHBusiness.PsManagementHandles
{
    public interface IPsManagementHandle : IPsManagementHandleCommon
    {
        /// <summary>
        /// 新增一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int ManagementInser(PsManagement management);
        /// <summary>
        /// 更新一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        int ManagementUpdate(PsManagement management);
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
        bool CheckManagementNameIsExist(string Name, long ManagementNo = -1);
        /// <summary>
        /// 检查编码是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        bool CheckManagementNoIsExist(long ManagementNo);
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        PsManagement GetManagementInfo(long ManagementNo);
    }
}
