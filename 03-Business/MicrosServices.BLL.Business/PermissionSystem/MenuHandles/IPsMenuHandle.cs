using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;

namespace MicrosServices.BLL.SHBusiness.PsMenuHandles
{
    public interface IPsMenuHandle : IPsMenuHandleCommon
    {
        /// <summary>
        /// 检查名称是否重复
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        bool CheckNameIsExist(long MenuNo, string Name);
        /// <summary>
        /// 检查菜单编号是否存在
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        bool CheckMenuNoIsExist(long MenuNo);
    }
}
