using MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryTypeHandle;
using MicrosServices.Entities.Common.BaseSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.BaseSystem.BsDictionaryTypeHandle
{
    public interface IBsDictionaryTypeHandle : IBsDictionaryTypeHandleCommon
    {
        /// <summary>
        /// 新增字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DictionaryTypeInsert(BsDictionaryType model);
        /// <summary>
        /// 更新字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DictionaryTypeUpdate(BsDictionaryType model);
    }
}
