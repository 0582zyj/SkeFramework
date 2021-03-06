using System;
using System.Data;
using System.Collections;
using System.Linq;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;

namespace MicrosServices.DAL.DataAccess.DataHandle.Repositorys
{
    public interface IUcUsersHandleCommon : IDataTableHandle<UcUsers>
  {
        /// <summary>
        /// 更新登录错误次数
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <param name="Zero">是否清0，1 True,0 False</param>
        /// <returns>受影响的行数</returns>
        int UpdateFailLogin(string UserNo, bool IsZero, string LoginerInfo);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        int DeleteUser(string UserNo);
    }
}
