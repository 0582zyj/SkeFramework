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

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsOrganizationHandleCommon : DataTableHandle<PsOrganization>, IPsOrganizationHandleCommon
    {
        public PsOrganizationHandleCommon(IRepository<PsOrganization> dataSerialer)
            : base(dataSerialer, PsOrganization.TableName, false)
        {
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        public int BatchDelete(long[] Nos)
        {
            if (Nos.Length > 0)
            {
                string param = String.Join(",", Nos);
                string sSQL = String.Format(@"DELETE FROM {0} where OrgNo in ({1})", _mTableName, param);
                return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL);
            }
            return 0;
        }
    }
}
