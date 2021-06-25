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
using System.Text;
using SkeFramework.Core.Common.Collections;

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

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues(List<long> PlatformNos)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format("SELECT OrgNo as Value,Name  FROM {0} WHERE 1=1 ", _mTableName);
            if (!CollectionUtils.IsEmpty(PlatformNos))
            {
                if (PlatformNos.Count == 1)
                {
                    sSQL += " AND PlatformNo=@PlatformNo  ";
                    ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNos[0]));
                }
                else
                {
                    sSQL += String.Format(" AND PlatformNo IN ({0})", String.Join(",", PlatformNos));
                }
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

        /// <summary>
        /// 获取所有子节点列表
        /// </summary>
        public List<PsOrganization> GetChildOrganizationList(long OrgNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT * FROM {0} ", _mTableName));
            sSQL.Append(" WHERE treeLevelNo like @OrgNo order by treeLevelNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@OrgNo", "%" + OrgNo + "%"));
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<PsOrganization>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<PsOrganization>();
        }

        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="OrgNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        public bool UpdateTreeLevelNo(long OrgNo, string TreeLevelNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"UPDATE {0} SET TreeLevelNo = @TreeLevelNo  ", _mTableName));
            sSQL.Append(" WHERE OrgNo=@OrgNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@TreeLevelNo", TreeLevelNo));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@OrgNo", OrgNo));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL.ToString(), ParaList.ToArray()) > 0;
        }
    }
}
