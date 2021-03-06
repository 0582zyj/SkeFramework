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
using System.Text;
using MicrosServices.Helper.Core.Extends;

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
                string sSQL = String.Format(@"DELETE FROM {0} where ManagementNo in ({1})", _mTableName, param);
                return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL);
            }
            return 0;
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues(long PlatformNo = ConstData.DefaultNo, long ManagementType = ConstData.DefaultNo,long ParentNo=ConstData.DefaultNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format("SELECT ManagementNo as Value,Name  FROM {0} ", _mTableName);
            if (PlatformNo != ConstData.DefaultNo)
            {
                sSQL += " WHERE PlatformNo=@PlatformNo ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            }
            if (ManagementType != ConstData.DefaultNo)
            {
                sSQL += " AND Type=@Type ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@Type", ManagementType));
            }
            if (ParentNo != ConstData.DefaultNo)
            {
                sSQL += " AND ParentNo=@ParentNo ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@ParentNo", ParentNo));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL, ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<OptionValue>();
        }

        /// <summary>
        /// 获取角色权限菜单列表
        /// </summary>
        /// <param name="RolesNos"></param>
        /// <returns></returns>
        public List<PsManagement> GetRoleManagementList(List<long> RolesNos)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT distinct pm.* FROM ps_management pm
                    LEFT JOIN ps_management_roles pmr on pmr.ManagementNo=pm.ManagementNo
                    WHERE  pm.Type=2 ", _mTableName, PsMenuManagement.TableName));
            if (RolesNos.Count == 1)
            {
                sSQL.Append(" AND pmr.RolesNo=@RolesNo ");
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@RolesNo", RolesNos[0]));
            }
            else if (RolesNos.Count > 1)
            {
                sSQL.Append(String.Format(" AND pmr.RolesNo IN ({0})", String.Join(",", RolesNos)));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<PsManagement>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<PsManagement>();
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <returns></returns>
        public List<ManagementOptionValue> GetManagementOptions(long PlatformNo = ConstData.DefaultNo, long ManagementType = ConstData.DefaultNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format("SELECT ManagementNo as Value,Name,Value as Code,Type  FROM {0} ", _mTableName);
            if (PlatformNo != ConstData.DefaultNo)
            {
                sSQL += " WHERE PlatformNo=@PlatformNo ";
            }
            else
            {
                sSQL += " WHERE PlatformNo<>@PlatformNo ";
            }
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            if (ManagementType != ConstData.DefaultNo)
            {
                sSQL += " AND Type=@Type ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@Type", ManagementType));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL, ParaList.ToArray());
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
