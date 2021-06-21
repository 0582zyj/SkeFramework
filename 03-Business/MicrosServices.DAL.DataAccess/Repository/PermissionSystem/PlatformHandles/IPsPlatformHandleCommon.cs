using System;
using System.Data;
using System.Collections;
using System.Linq;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Entities.Constants;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsPlatformHandleCommon : IDataTableHandle<PsPlatform>
    {
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        List<OptionValue> GetOptionValues(long Plarform=ConstData.DefaultNo);
        /// <summary>
        /// 获取所有子节点列表
        /// </summary>
        List<PsPlatform> GetChildPlatformList(long PlarformNo);
        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="PlarformNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        bool UpdateTreeLevelNo(long PlarformNo, string TreeLevelNo);        
    }
}
