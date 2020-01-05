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

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class PsMenuManagementHandleCommon : DataTableHandle<PsMenuManagement>, IPsMenuManagementHandleCommon
  {
        public PsMenuManagementHandleCommon(IRepository<PsMenuManagement> dataSerialer)
            : base(dataSerialer, PsMenuManagement.TableName, false)
        {
        }
        /// <summary>
        /// 删除权限菜单列表
        /// </summary>
        /// <param name="managementNo"></param>
        public bool DeleteManagementMenus(long managementNo)
        {
            string sSQL = String.Format(@"DELETE FROM {0} WHERE ManagementNo=@ManagementNo", _mTableName);
            DbParameter parameter = DbFactory.Instance().CreateDataParameter("ManagementNo", managementNo);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, parameter) > 0 ? true : false;
        }
    }
}
