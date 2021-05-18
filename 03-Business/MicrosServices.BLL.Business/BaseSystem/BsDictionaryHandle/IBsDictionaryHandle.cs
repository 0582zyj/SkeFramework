using MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryHandle;
using MicrosServices.Entities.Common.BaseSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.BaseSystem.BsDictionaryHandle
{
    public interface IBsDictionaryHandle : IBsDictionaryHandleCommon
    {
        /// <summary>
        /// 新增字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DictionaryInsert(BsDictionary model);
        /// <summary>
        /// 更新字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DictionaryUpdate(BsDictionary model);
        /// <summary>
        /// 检查字段类型是否已有字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        bool CheckDictionaryTypeIsExist(string dicType);

    }
}
