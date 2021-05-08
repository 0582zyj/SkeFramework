using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryTypeHandle
{
    public interface IBsDictionaryTypeHandleCommon : IDataTableHandle<BsDictionaryType>
    {
        /// <summary>
        /// 根据字典类型获取键值对
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        List<DictionaryOptionValue> GetOptionValues(long PlatformNo = ConstData.DefaultNo);
    }
}

