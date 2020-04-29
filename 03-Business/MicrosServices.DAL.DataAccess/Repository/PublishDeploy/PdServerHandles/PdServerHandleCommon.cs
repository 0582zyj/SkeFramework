using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using Newtonsoft.Json;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdServerHandles
{
    public class PdServerHandleCommon : DataTableHandle<PdServer>, IPdServerHandleCommon
    {
        public PdServerHandleCommon(IRepository<PdServer> dataSerialer)
            : base(dataSerialer, PdServer.TableName, false)
        {
        }

        public List<OptionValue> GetOptionValues()
        {
            List<DbParameter> parameters = new List<DbParameter>();
            string sSQL = "SELECT ServerNo as Value,Name  FROM pd_server; ";
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL, parameters.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<OptionValue>();
        }
    }
}
