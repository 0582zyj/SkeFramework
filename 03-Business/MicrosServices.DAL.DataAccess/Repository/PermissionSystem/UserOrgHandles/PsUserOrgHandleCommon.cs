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
    public class PsUserOrgHandleCommon : DataTableHandle<PsUserOrg>, IPsUserOrgHandleCommon
    {
        public PsUserOrgHandleCommon(IRepository<PsUserOrg> dataSerialer)
            : base(dataSerialer, PsUserOrg.TableName, false)
        {
        }
        /// <summary>
        /// 删除用户机构列表
        /// </summary>
        /// <param name="managementNo"></param>
        public bool DeleteUserOrgs(string UserNo)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE UserNo=@UserNo", _mTableName);
            DbParameter parameter = DbFactory.Instance().CreateDataParameter("UserNo", UserNo);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, parameter) > 0 ? true : false;
        }
    }
}
