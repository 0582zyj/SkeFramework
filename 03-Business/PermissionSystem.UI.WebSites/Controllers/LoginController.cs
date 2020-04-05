using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.UserCenter.FORM;
using MicrosServices.SDK.UserCenter;
using Newtonsoft.Json;
using PermissionSystem.UI.WebSites.Global;
using PermissionSystem.UI.WebSites.Models;
using SkeFramework.Cache.Redis;
using SkeFramework.Cache.Redis.Entities;
using SkeFramework.Core.Encrypts;
using SkeFramework.Core.Network.Responses;

namespace SmartCloudIOT.UI.WebSite.Controllers
{
    public class LoginController : Controller
    {
        private LoginSdk loginSdk = new LoginSdk();
        private UserSettingSdk userSettingSdk = new UserSettingSdk();

        #region 管理后台
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (AppBusiness.loginModel != null)
            {
                System.Web.HttpContext.Current.Session.Remove("LoginModel");
            }
            if (AppBusiness.SideBarList != null)
            {
                System.Web.HttpContext.Current.Session.Remove("SideBarList");
            }
            return RedirectToAction("Login", "Login");
        }
        #endregion

        /// <summary>
        /// 登录提交方法
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Captcha"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login( string UserName, string Password, string Captcha)
        {
            LoginInfoForm loginInfoForm = new LoginInfoForm();
            loginInfoForm.UserName = UserName;
            loginInfoForm.Password = Password;
            loginInfoForm.LoginerInfo = "123";
            loginInfoForm.Platform = AppBusiness.PlatformCode;
            JsonResponses responses = loginSdk.Login(loginInfoForm);
            if (responses.code == JsonResponses.Success.code)
            {
                UcUsers users =JsonConvert.DeserializeObject<UcUsers>( responses.data.ToString());
                UcUsersSetting usersSetting = userSettingSdk.GetUserSettingInfo(users.UserNo);
                if (usersSetting != null)
                {
                    string APPKey = usersSetting.AppSecret;
                    LoginModel.Instance().UserNo = users.UserNo;
                    LoginModel.Instance().Token = MD5Helper.GetMD5String(users.UserNo + APPKey + DateTime.Now.ToString("yyyyMMddHHmmss")); ;
                    LoginModel.Instance().ManagementValue = 1213;// roles.ManagementValue;
                    LoginModel.Instance().UserRolesName = "123";// roles.Name;
                    LoginModel.Instance().UserRule = "123";//DataHandleManager.Instance().UsersRuleHandle.GetUserRoles(UserNo);
                    LoginModel.Instance().PlatformNo = usersSetting.PlatformNo;
                    AppBusiness.loginModel = LoginModel.Instance();
                }
                else
                {
                    responses.code = JsonResponses.FailedCode;
                    responses.msg = "用户信息缺失";
                }
            }
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

        #region 用户前台
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult WebLogin()
        {
            return View();
        }
        #endregion 
    }
}
