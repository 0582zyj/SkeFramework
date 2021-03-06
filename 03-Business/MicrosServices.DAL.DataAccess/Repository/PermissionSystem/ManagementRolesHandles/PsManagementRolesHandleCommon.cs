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
    public class PsManagementRolesHandleCommon : DataTableHandle<PsManagementRoles>, IPsManagementRolesHandleCommon
  {
        public PsManagementRolesHandleCommon(IRepository<PsManagementRoles> dataSerialer)
            : base(dataSerialer, PsManagementRoles.TableName, false)
        {
        }

        /// <summary>
        /// 删除角色的权限
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        public bool DeleteManagementRoles(long RolesNo)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE RolesNo=@RolesNo", _mTableName);
            DbParameter parameter=DbFactory.Instance().CreateDataParameter("RolesNo", RolesNo);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text,sSQL, parameter)>0?true:false;
        }
    }
}
