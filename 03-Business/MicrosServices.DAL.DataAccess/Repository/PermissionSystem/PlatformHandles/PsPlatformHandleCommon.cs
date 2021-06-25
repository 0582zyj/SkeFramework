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
        public List<OptionValue> GetOptionValues(long PlatformNo = ConstData.DefaultNo)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            string sSQL = "SELECT PlatformNo as Value,Name  FROM ps_platform; ";
            if (PlatformNo  != ConstData.DefaultNo)
            {
                sSQL += " where TreeLevelNo like @TreeLevelNo or PlatformNo=@PlatformNo;";
                parameters.Add( DbFactory.Instance().CreateDataParameter("@TreeLevelNo", "'%"+ PlatformNo  + "%'"));
                parameters.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo ));
            }
            DataTable dataTable= RepositoryHelper.GetDataTable(CommandType.Text, sSQL, parameters.ToArray());
            if(dataTable!=null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<OptionValue>();
        }

        /// <summary>
        /// 获取所有子节点列表
        /// </summary>
        public List<PsPlatform> GetChildPlatformList(long PlatformNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT * FROM {0} ", _mTableName));
            sSQL.Append(" WHERE treeLevelNo like @PlatformNo order by treeLevelNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", "%" + PlatformNo + "%"));
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<PsPlatform>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<PsPlatform>();
        }

        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        public bool UpdateTreeLevelNo(long PlatformNo, string TreeLevelNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"UPDATE {0} SET TreeLevelNo = @TreeLevelNo  ", _mTableName));
            sSQL.Append(" WHERE PlatformNo=@PlatformNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@TreeLevelNo", TreeLevelNo));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL.ToString(), ParaList.ToArray()) > 0;
        }

        /// <summary>
        /// 获取平台编号列表
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        public List<long> GetChildPlatformNoList(long PlatformNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT PlatformNo FROM {0} ", _mTableName));
            sSQL.Append(" WHERE treeLevelNo like @PlatformNos or PlatformNo=@PlatformNo order by treeLevelNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNos", "%" + PlatformNo + "%"));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<long>>(JsonConvert.SerializeObject(dataTable.Select().Select(o => o[0])));
            }
            return new List<long>();
        }
    }
}
