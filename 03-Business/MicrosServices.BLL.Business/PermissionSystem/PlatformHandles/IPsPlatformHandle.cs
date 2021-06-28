using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using System.Collections.Generic;

namespace MicrosServices.BLL.SHBusiness.PsPlatformHandles
{
    public interface IPsPlatformHandle : IPsPlatformHandleCommon
    {

        /// <summary>
        /// 获取平台信息
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        PsPlatform GetPlatformInfo(long PlatformNo);
        /// <summary>
        /// 平台更新
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        int PlatformUpdate(PsPlatform platform);
        

        /// <summary>
        /// 刷新子节点编号路径
        /// </summary>
        /// <param name="current"></param>
        void RefreshChildTreeLevelNo(PsPlatform current);
        /// <summary>
        /// 删除平台
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeletePlatform(int id);
        /// <summary>
        /// 获取上级父平台节点编号
        /// </summary>
        /// <param name="userPlatfromNo"></param>
        /// <returns></returns>
        List<long> GetParentPlatformNos(long userPlatfromNo);
    }
}
