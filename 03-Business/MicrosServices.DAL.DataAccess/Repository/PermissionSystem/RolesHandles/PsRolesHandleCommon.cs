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
using MicrosServices.Helper.Core.Common;
using Newtonsoft.Json;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    /// <summary>
    /// 角色数据库访问类
    /// </summary>
    public class PsRolesHandleCommon : DataTableHandle<PsRoles>, IPsRolesHandleCommon
  {
        public PsRolesHandleCommon(IRepository<PsRoles> dataSerialer)
            : base(dataSerialer, PsRoles.TableName, false)
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
                string sSQL = String.Format(@"DELETE FROM {0} where RolesNo in ({1})", _mTableName, param);
                return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL);
            }
            return 0;
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues()
        {
            string sSQL = String.Format("SELECT MenuNo as Value,Name  FROM {0}; ", _mTableName);
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL);
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            else
            {
                return new List<OptionValue>();
            }
        }

    }
}
