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
    }
}
