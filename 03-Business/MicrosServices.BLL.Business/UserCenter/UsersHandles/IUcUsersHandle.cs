using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;

namespace MicrosServices.BLL.SHBusiness.UsersHandles
{
    public interface IUcUsersHandle : IUcUsersHandleCommon
    {
        /// <summary>
        /// 检查用户账号是否存在
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        bool CheckUserNoIsExist(string UserNo);
        /// <summary>
        /// 检查邮箱是否存在
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        bool CheckEmailIsExist(string Email);
        /// <summary>
        /// 检查电话号码是否存在
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        bool CheckPhoneIsExist(string Phone);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="usersNo"></param>
        /// <returns></returns>
        UcUsers GetUsersInfo(string usersNo);
        /// <summary>
        /// 平台登录业务
        /// </summary>
        /// <param name="loginForm"></param>
        /// <returns></returns>
        LoginResultType PlatformLogin(LoginInfoForm loginForm, ref UcUsers info);
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
        /// <summary>
        /// 平台注册用户
        /// </summary>
        /// <param name="registerPlatform"></param>
        /// <returns></returns>
        LoginResultType RegisterPlatform(RegisterPlatformForm registerPlatform);
        /// <summary>
        /// 平台注销用户
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        LoginResultType CancelPlatform(string UserNo);
    }
}
