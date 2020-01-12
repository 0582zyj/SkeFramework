using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.UserCenter.UcUsersSettingHandles
{
    public class UcUsersSettingHandleCommon : DataTableHandle<UcUsersSetting>, IUcUsersSettingHandleCommon
    {
        public UcUsersSettingHandleCommon(IRepository<UcUsersSetting> dataSerialer)
            : base(dataSerialer, UcUsersSetting.TableName, false)
        {
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public int DeleteUserSetting(string UserNo)
        {
            string sql = string.Format("DELETE FROM {0}  where UserNo=@UserNo", this._mTableName);//自增1;
            List<DbParameter> ParaList = new List<DbParameter>();
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@UserNo", UserNo));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sql, ParaList.ToArray());
        }
    }

}
