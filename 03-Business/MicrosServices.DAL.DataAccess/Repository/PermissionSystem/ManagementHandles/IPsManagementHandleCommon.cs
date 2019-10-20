using System;
using System.Data;
using System.Collections;
using System.Linq;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;

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
    }
}
