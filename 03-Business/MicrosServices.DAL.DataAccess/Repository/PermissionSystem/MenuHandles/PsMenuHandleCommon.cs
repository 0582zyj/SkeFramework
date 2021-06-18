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
using System.Text;

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
            return new List<OptionValue>();
        }


        /// <summary>
        /// 根据账号获取菜单列表
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public List<PsMenu> GetManagementMenusList(List<long> ManagementNos)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT distinct psm.* FROM {0} psm
                  LEFT JOIN {1} pmm On pmm.MenuNo =psm.MenuNo", _mTableName,PsMenuManagement.TableName));
            if (  ManagementNos.Count==1)
            {
                sSQL.Append( " WHERE pmm.ManagementNo=@ManagementNo");
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@ManagementNo", ManagementNos[0]));
            }
            else if (ManagementNos.Count > 1)
            {
                sSQL.Append(String.Format(" WHERE pmm.ManagementNo IN ({0})", String.Join(",", ManagementNos)));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<PsMenu>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<PsMenu>();
        }

        /// <summary>
        /// 获取所有子菜单列表
        /// </summary>
        public List<PsMenu> GetChildMenuList(long MenuNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT * FROM {0}  ", _mTableName));
            sSQL.Append(" WHERE treeLevelNo like @MenuNo order by treeLevelNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@MenuNo", "%" + MenuNo + "%"));
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<PsMenu>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<PsMenu>();
        }

        /// <summary>
        /// 更新节点路径
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <param name="TreeLevelNo"></param>
        /// <returns></returns>
        public bool UpdateTreeLevelNo(long MenuNo, string TreeLevelNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"UPDATE {0} SET TreeLevelNo = @TreeLevelNo  ", _mTableName));
            sSQL.Append(" WHERE MenuNo=@MenuNo;");
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@TreeLevelNo", TreeLevelNo));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@MenuNo", MenuNo));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL.ToString(), ParaList.ToArray())>0;
        }
    }
}
