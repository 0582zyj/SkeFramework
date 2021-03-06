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
    }
}
