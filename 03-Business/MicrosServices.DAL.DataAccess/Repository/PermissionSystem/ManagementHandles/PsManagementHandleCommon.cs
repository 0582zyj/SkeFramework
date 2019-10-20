using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.Common.DataCommon;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsManagementHandleCommon : DataTableHandle<PsManagement>, IPsManagementHandleCommon
  {
        public PsManagementHandleCommon(IRepository<PsManagement> dataSerialer)
            : base(dataSerialer, PsManagement.TableName, false)
        {
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ManagementNos"></param>
        /// <returns></returns>
        public int BatchDelete(long[] ManagementNos)
        {
            if (ManagementNos.Length > 0)
            {
                string param = String.Join(",", ManagementNos);
                string sSQL =String.Format( @"DELETE FROM {0} where ManagementNo in ({1})",_mTableName, param);
                return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL);
            }
            return 0;
        }
    }
}
