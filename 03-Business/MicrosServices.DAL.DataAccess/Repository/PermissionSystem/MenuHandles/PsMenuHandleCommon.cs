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
using System.Data.Common;
using MicrosServices.Entities.Constants;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsMenuHandleCommon : DataTableHandle<PsMenu>, IPsMenuHandleCommon
    {
        public PsMenuHandleCommon(IRepository<PsMenu> dataSerialer)
            : base(dataSerialer, PsMenu.TableName, false)
        {
        }
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues(long PlatformNo = -1)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL =String.Format( "SELECT MenuNo as Value,Name  FROM {0} ",_mTableName);
            if (PlatformNo != ConstData.DefaultNo)
            {
                sSQL += " WHERE PlatformNo=@PlatformNo ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL,ParaList.ToArray());
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
