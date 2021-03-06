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
using MicrosServices.Entities.Constants;
using System.Data.Common;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsPlatformHandleCommon : DataTableHandle<PsPlatform>, IPsPlatformHandleCommon
    {
        public PsPlatformHandleCommon(IRepository<PsPlatform> dataSerialer)
            : base(dataSerialer, PsPlatform.TableName, false)
        {
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues(long Plarform= ConstData.DefaultNo)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            string sSQL = "SELECT PlatformNo as Value,Name  FROM ps_platform; ";
            if (Plarform != ConstData.DefaultNo)
            {
                sSQL += " where TreeLevelNo like @TreeLevelNo or PlatformNo=@PlatformNo;";
                parameters.Add( DbFactory.Instance().CreateDataParameter("@TreeLevelNo", "'%"+ Plarform + "%'"));
                parameters.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", Plarform));
            }
            DataTable dataTable= RepositoryHelper.GetDataTable(CommandType.Text, sSQL, parameters.ToArray());
            if(dataTable!=null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<OptionValue>();
        }
    }
}
