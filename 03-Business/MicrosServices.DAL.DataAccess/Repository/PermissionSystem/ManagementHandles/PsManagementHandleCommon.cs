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
using Newtonsoft.Json;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Entities.Constants;
using System.Data.Common;

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

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues(long PlatformNo = ConstData.DefaultNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format("SELECT ManagementNo as Value,Name  FROM {0} ", _mTableName);
            if(PlatformNo!= ConstData.DefaultNo)
            {
                sSQL += " WHERE PlatformNo=@PlatformNo ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL, ParaList.ToArray());
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
