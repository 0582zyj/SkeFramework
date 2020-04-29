using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using Newtonsoft.Json;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdProjectHandles
{
    public class PdProjectHandleCommon : DataTableHandle<PdProject>, IPdProjectHandleCommon
    {
        public PdProjectHandleCommon(IRepository<PdProject> dataSerialer)
            : base(dataSerialer, PdProject.TableName, false)
        {
        }


        public List<OptionValue> GetOptionValues()
        {
            string sSQL = String.Format("SELECT ProjectNo as Value,Name  FROM {0} ", _mTableName);
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL);
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<OptionValue>();
        }
    }
}
