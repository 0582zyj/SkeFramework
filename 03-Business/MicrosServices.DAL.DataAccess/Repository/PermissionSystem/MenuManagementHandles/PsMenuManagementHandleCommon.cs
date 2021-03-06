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
using MicrosServices.Helper.Core.Extends;
using MicrosServices.Entities.Constants;
using Newtonsoft.Json;
using System.Text;
using MicrosServices.Helper.Core.Constants;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsMenuManagementHandleCommon : DataTableHandle<PsMenuManagement>, IPsMenuManagementHandleCommon
  {
        public PsMenuManagementHandleCommon(IRepository<PsMenuManagement> dataSerialer)
            : base(dataSerialer, PsMenuManagement.TableName, false)
        {
        }
       
        /// <summary>
        /// 删除菜单权限列表
        /// </summary>
        /// <param name="managementNo"></param>
        public bool DeleteManagementMenus(long ManagementNo, int Type)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE ManagementNo=@ManagementNo AND Type=@Type", _mTableName);
            List<DbParameter> ParaList = new List<DbParameter>();
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@ManagementNo", ManagementNo));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@Type", Type));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, ParaList.ToArray()) > 0 ? true : false;
        }
        /// <summary>
        /// 删除菜单权限列表
        /// </summary>
        /// <param name="managementNo"></param>
        public bool DeleteMenuManagements(long MenuNo,int Type)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE MenuNo=@MenuNo AND Type=@Type", _mTableName);
            List<DbParameter> ParaList = new List<DbParameter>();
            ParaList.Add( DbFactory.Instance().CreateDataParameter("@MenuNo", MenuNo));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@Type", Type));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, ParaList.ToArray()) > 0 ? true : false;
        }


        /// <summary>
        /// 获取菜单权限操作列表
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        public List<ManagementOptionValue> GetManagementOptionValues(List<long> MenuNos, int ManagementType)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL =new StringBuilder( String.Format(@"SELECT pm.ManagementNo as Value ,pm.Name,Value as Code,  pm.Type as Type FROM {0} pm
                                        LEFT JOIN  {1} pmm   on pmm.ManagementNo = pm.ManagementNo
                                        WHERE   pmm.Type = @Type AND pm.Enabled=@Enabled ", PsManagement.TableName, _mTableName));
            if (MenuNos.Count == 1)
            {
                sSQL.Append(" AND pmm.MenuNo = @MenuNo ");
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@MenuNo", MenuNos[0]));
            }
            else if (MenuNos.Count > 1)
            {
                sSQL.Append(String.Format(" AND pmm.MenuNo IN ({0})", String.Join(",", MenuNos)));
            }
            else
            {
                return new List<ManagementOptionValue>();
            }
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@Type", ManagementType));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@Enabled", (int)EnabledType.ACTIVE));
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<ManagementOptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            else
            {
                return new List<ManagementOptionValue>();
            }
        }
    }
}
