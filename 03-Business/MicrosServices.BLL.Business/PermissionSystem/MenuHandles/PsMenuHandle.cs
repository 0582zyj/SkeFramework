using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;

namespace MicrosServices.BLL.SHBusiness.PsMenuHandles
{
    public class PsMenuHandle : PsMenuHandleCommon, IPsMenuHandle
    {
        public PsMenuHandle(IRepository<PsMenu> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 检查名称是否重复
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool CheckNameIsExist(long MenuNo, string Name)
        {
            Expression<Func<PsMenu, bool>> where = (n => n.MenuNo != MenuNo && n.Name == Name);
            return this.Count(where) > 0 ? true : false;
        }

        /// <summary>
        /// 检查菜单编号是否存在
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        public bool CheckMenuNoIsExist(long MenuNo)
        {
            if (MenuNo != -1)
            {
                Expression<Func<PsMenu, bool>> where = (n => n.MenuNo == MenuNo);
                return this.Count(where) > 0 ? true : false;
            }
            return false;
        }
    }
}
