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
    /// <summary>
    /// 角色数据库访问类
    /// </summary>
    public class PsRolesHandleCommon : DataTableHandle<PsRoles>, IPsRolesHandleCommon
    {
        public PsRolesHandleCommon(IRepository<PsRoles> dataSerialer)
            : base(dataSerialer, PsRoles.TableName, false)
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
                string sSQL = String.Format(@"DELETE FROM {0} where RolesNo in ({1})", _mTableName, param);
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
            string sSQL = String.Format("SELECT RolesNo as Value,Name  FROM {0} ", _mTableName);
            if (PlatformNo != ConstData.DefaultNo)
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
        /// <summary>
        /// 获取获取用户角色列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public List<PsRoles> GetUserRoleList(string UserNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            StringBuilder sSQL = new StringBuilder(String.Format(
                @"SELECT * FROM  {0} pr 
                    LEFT join {1} pur ON pur.RolesNo=pr.RolesNo "
                    , _mTableName, PsUserRoles.TableName));
            if (!UserNo.Equals(ConstData.DefaultNo.ToString()))
            {
                sSQL.Append(" WHERE UserNo=@UserNo ");
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@UserNo", UserNo));
            }
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL.ToString(), ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<PsRoles>>(JsonConvert.SerializeObject(dataTable));
            }
            else
            {
                return new List<PsRoles>();
            }
        }
    }
}
