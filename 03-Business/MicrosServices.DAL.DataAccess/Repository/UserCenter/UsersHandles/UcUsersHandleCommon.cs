using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using System.Data.Common;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.Common.DataCommon;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public class UcUsersHandleCommon : DataTableHandle<UcUsers>, IUcUsersHandleCommon
    {
        public UcUsersHandleCommon(IRepository<UcUsers> dataSerialer)
            : base(dataSerialer, UcUsers.TableName, false)
        {
        }


        /// <summary>
        /// 更新登录错误次数
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <param name="Zero">是否清0，1 True,0 False</param>
        /// <returns>受影响的行数</returns>
        public int UpdateFailLogin(string UserNo, bool IsZero, string LoginerInfo)
        {
            string sql = "";
            if (IsZero)
            {
                sql = string.Format(@"UPDATE {0} SET FailedLoginCount = 0 ,FailedLoginTime = @FailedLoginTime WHERE UserNo=@UserNo", this._mTableName);//清零
            }
            else
            {
                sql = string.Format("update {0} set FailedLoginCount=FailedLoginCount+1,FailedLoginTime=@FailedLoginTime where UserNo=@UserNo", this._mTableName);//自增1
            }
            List<DbParameter> ParaList = new List<DbParameter>();
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@FailedLoginTime", DateTime.Now));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@UserNo", UserNo));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sql, ParaList.ToArray());
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public int DeleteUser(string UserNo)
        {
            string sql = string.Format("DELETE FROM {0}  where UserNo=@UserNo", this._mTableName);//自增1;
            List<DbParameter> ParaList = new List<DbParameter>();
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@UserNo", UserNo));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sql, ParaList.ToArray());
        }
    }
}
