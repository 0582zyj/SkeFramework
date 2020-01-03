using System;
using System.Data;
using System.Collections;
using System.Linq;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IPsManagementHandleCommon : IDataTableHandle<PsManagement>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ManagementNos"></param>
        /// <returns></returns>
        int BatchDelete(long[] ManagementNos);
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        List<OptionValue> GetOptionValues(long PlatformNo=-1);
    }
}
