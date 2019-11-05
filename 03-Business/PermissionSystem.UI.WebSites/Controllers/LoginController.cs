using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.UserCenter.FORM;
using MicrosServices.SDK.UserCenter;
using PermissionSystem.UI.WebSites.Global;
using PermissionSystem.UI.WebSites.Models;
using SkeFramework.Core.Network.Responses;

namespace SmartCloudIOT.UI.WebSite.Controllers
{
    public class LoginController : Controller
    {
        private LoginSdk loginSdk = new LoginSdk();
        #region 管理后台
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
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
            //if (AppBusiness.SideBarList != null)
            //{
            //    System.Web.HttpContext.Current.Session.Remove("SideBarList");
            //}
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
        public JsonResult Login(string UserName, string Password, string Captcha)
        {
            LoginInfoForm loginInfoForm = new LoginInfoForm();
            loginInfoForm.UserName = UserName;
            loginInfoForm.Password = Password;
            loginInfoForm.LoginerInfo = "123";
            loginInfoForm.Platform = "123";
            JsonResponses responses = loginSdk.Login(loginInfoForm);
            //    ManagementRoles roles = DataHandleManager.Instance().ManagementRolesHandle.GetModelByKey(RolesID.ToString());
            LoginModel.Instance().UserNo = "123";
            LoginModel.Instance().Token = "123";// MD5Helper.GetMD5String(UserNo + APPKey + DateTime.Now.ToString("yyyyMMddHHmmss"));;
            LoginModel.Instance().ManagementValue = 1213;// roles.ManagementValue;
            LoginModel.Instance().UserRolesName = "123";// roles.Name;
            LoginModel.Instance().UserRule = "123";//DataHandleManager.Instance().UsersRuleHandle.GetUserRoles(UserNo);
            AppBusiness.loginModel = LoginModel.Instance();
            //}
            return Json(400, JsonRequestBehavior.AllowGet);
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
