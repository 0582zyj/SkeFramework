using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using SkeFramework.DataBase.Interfaces;
using System.Linq.Expressions;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
using MicrosServices.Entities.Constants;
using SkeFramework.Core.SnowFlake;
using MicrosServices.BLL.Business;

namespace MicrosServices.BLL.SHBusiness.UsersHandles
{
    public class UcUsersHandle : UcUsersHandleCommon, IUcUsersHandle
    {
        public UcUsersHandle(IRepository<UcUsers> dataSerialer)
            : base(dataSerialer)
        {
        }

        #region 检查方法
        /// <summary>
        /// 检查用户账号是否存在
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public bool CheckUserNoIsExist(string UserNo)
        {
            if (UserNo != ConstData.DefaultNo.ToString())
            {
                Expression<Func<UcUsers, bool>> where = (o => o.UserNo == UserNo);
                long count = this.Count(where);
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查邮箱是否存在
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public bool CheckEmailIsExist(string Email)
        {
            if (Email != ConstData.DefaultNo.ToString())
            {
                Expression<Func<UcUsers, bool>> where = (o => o.Email == Email);
                long count = this.Count(where);
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查电话号码是否存在
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public bool CheckPhoneIsExist(string Phone)
        {
            if (Phone != ConstData.DefaultNo.ToString())
            {
                Expression<Func<UcUsers, bool>> where = (o => o.Phone == Phone);
                long count = this.Count(where);
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 登录方法
        /// <summary>
        /// 登陆业务
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Md5Pas">密码</param>
        /// <param name="LoginerInfo">登陆者信息</param>
        /// <param name="platform">平台code</param>
        /// <param name="info"></param>
        /// <returns></returns>
        public LoginResultType Login(string UserName, string Md5Pas, string LoginerInfo, string platform, ref UcUsers info)
        {
            if (UserName == "" || Md5Pas == "" || UserName == null || Md5Pas == null)
            {
                return LoginResultType.ERROR_PARA; //账号或者密码为空
            }
            Expression<Func<UcUsers, bool>> la = (n => n.Phone == UserName || n.Email == UserName || n.UserName == UserName);
            info = this.Get(la);
            if (info == null || string.IsNullOrEmpty(info.UserNo))
            {
                return LoginResultType.ERROR_USER_NOT_EXIST;//登录账号不存在
            }
            if (info.Enabled == 0)
            {
                return LoginResultType.ERROR_USER_INACTIVE;//账号停用
            }
            if (info.FailedLoginCount >= 3)
            {
                return LoginResultType.ERROR_PASSWORD_TOO_MUCH;//密码输入错误次数超过三次
            }
            string UserNo = info.UserNo;
            if (Md5Pas != info.Password)
            {
                this.UpdateFailLogin(UserNo, false, LoginerInfo);//更新错误登录次数
                return LoginResultType.ERROR_PASSWORD;//密码错误
            }
            this.UpdateFailLogin(UserNo, true, LoginerInfo);//清零错误登录次数
            return LoginResultType.SUCCESS_LOGIN;
        }
        #endregion

        #region 注册方法
        /// <summary>
        /// 平台注册用户
        /// </summary>
        /// <param name="registerPlatform"></param>
        /// <returns></returns>
        public LoginResultType RegisterPlatform(RegisterPlatformForm registerPlatform)
        {
            if (CheckUserNoIsExist(registerPlatform.UserNo))
            {
                return LoginResultType.ERROR_USER_NOT_EXIST;
            }
            if (CheckEmailIsExist(registerPlatform.Email))
            {
                return LoginResultType.ERROR_EMAIL_NOT_EXIST;
            }
            if (CheckPhoneIsExist(registerPlatform.Phone))
            {
                return LoginResultType.ERROR_PHONE_NOT_EXIST;
            }

            string UserNo = registerPlatform.UserNo;
            if (UserNo.Equals(ConstData.DefaultNo))
            {
                UserNo = AutoIDWorker.Example.GetAutoSequence().ToString();
            }
            UcUsers users = new UcUsers()
            {
                UserNo = UserNo,
                UserName = registerPlatform.UserName,
                Password = registerPlatform.Password,
                Phone = registerPlatform.Phone,
                Email = registerPlatform.Email,
                InputUser=registerPlatform.InputUser,
                InputTime=DateTime.Now,
            };
            int result = DataHandleManager.Instance().UcUsersHandle.Insert(users);
            if (result > 0)
            {
                registerPlatform.UserNo = UserNo;
                return LoginResultType.SUCCESS_REGISTOR;
            }
            return LoginResultType.FAILED_REGISTOR;
        }
        #endregion
    }
}
