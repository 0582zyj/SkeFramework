using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.Interfaces;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using MicrosServices.Entities.Common;
using System.Data.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsUserRolesHandleCommon : DataTableHandle<PsUserRoles>, IPsUserRolesHandleCommon
    {
        public PsUserRolesHandleCommon(IRepository<PsUserRoles> dataSerialer)
            : base(dataSerialer, PsUserRoles.TableName, false)
        {
        }

        /// <summary>
        /// 删除用户角色列表
        /// </summary>
        /// <param name="managementNo"></param>
        public bool DeleteUserRoles(string UserNo)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE UserNo=@UserNo", _mTableName);
            DbParameter parameter = DbFactory.Instance().CreateDataParameter("UserNo", UserNo);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, parameter) > 0 ? true : false;
        }
    }
}
