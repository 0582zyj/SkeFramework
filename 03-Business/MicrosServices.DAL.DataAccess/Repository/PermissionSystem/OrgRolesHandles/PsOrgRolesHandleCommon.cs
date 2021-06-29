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
    public class PsOrgRolesHandleCommon : DataTableHandle<PsOrgRoles>, IPsOrgRolesHandleCommon
    {
        public PsOrgRolesHandleCommon(IRepository<PsOrgRoles> dataSerialer)
            : base(dataSerialer, PsOrgRoles.TableName, false)
        {
        }

        /// <summary>
        /// 删除用户角色列表
        /// </summary>
        /// <param name="managementNo"></param>
        public bool  DeleteOrgRoles(long OrgNo)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE OrgNo=@OrgNo", _mTableName);
            DbParameter parameter = DbFactory.Instance().CreateDataParameter("@OrgNo", OrgNo);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, parameter) > 0 ? true : false;
        }

        /// <summary>
        /// 删除角色机构
        /// </summary>
        /// <param name="OrgNo"></param>
        /// <returns></returns>
        public bool DeleteOrgRolesByRolesNo(long rolesNo)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE RolesNo=@RolesNo", _mTableName);
            DbParameter parameter = DbFactory.Instance().CreateDataParameter("@RolesNo", rolesNo);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, parameter) > 0 ? true : false;
        }

    }
}
