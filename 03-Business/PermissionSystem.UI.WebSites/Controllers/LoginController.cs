using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicrosServices.BLL.Business;
using PermissionSystem.UI.WebSites.Global;
using PermissionSystem.UI.WebSites.Models;

namespace SmartCloudIOT.UI.WebSite.Controllers
{
    public class LoginController : Controller
    {
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
            //UsersCloud userCloud =null;
            //UsersApp userApp = null;
            //string Md5Pas = MD5Helper.GetMD5String(Password);

            //int LoginResult = DataHandleManager.Instance().UsersCloudHandle.Login(UserName, Md5Pas, "", "W", ref userCloud);
            //if (LoginResult == 911)//账号不存在则去检查APP账号登录
            //{
            //    LoginResult = DataHandleManager.Instance().UsersAppHandle.Login(UserName, Md5Pas, "", "W", ref userApp);
            //}
            //if (LoginResult == 100)
            //{
            //    string UserNo = userCloud == null ? userApp.UserNo : userCloud.UserNo;
            //    int RolesID = userCloud == null ? userApp.RolesID : userCloud.RolesID;
            //    string APPKey = userCloud == null ? userApp.APPKey : userCloud.APPKey;

            //    ManagementRoles roles = DataHandleManager.Instance().ManagementRolesHandle.GetModelByKey(RolesID.ToString());
            LoginModel.Instance().UserNo = "123";
            LoginModel.Instance().Token = "123";// MD5Helper.GetMD5String(UserNo + APPKey + DateTime.Now.ToString("yyyyMMddHHmmss"));;
            LoginModel.Instance().ManagementValue = 1213;// roles.ManagementValue;
            LoginModel.Instance().UserRolesName = "123";// roles.Name;
            LoginModel.Instance().UserRule = "123";//DataHandleManager.Instance().UsersRuleHandle.GetUserRoles(UserNo);
            AppBusiness.loginModel = LoginModel.Instance();
            //}
            return Json(100, JsonRequestBehavior.AllowGet);
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
