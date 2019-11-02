using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Constants;

namespace MicrosServices.BLL.SHBusiness.UsersHandles
{
    public interface IUcUsersHandle : IUcUsersHandleCommon
    {
        /// <summary>
        /// 登陆业务
        /// </summary>
        /// <param name="UserName">用户名/邮箱/电话</param>
        /// <param name="Md5Pas"></param>
        /// <param name="LoginerInfo"></param>
        /// <param name="platform"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        LoginResultType Login(string UserName, string Md5Pas, string LoginerInfo, string platform, ref UcUsers info);
    }
}
