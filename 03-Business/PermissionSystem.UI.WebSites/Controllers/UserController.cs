using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.UserCenter.FORM;
using MicrosServices.Helper.Core.VO;
using MicrosServices.SDK.PermissionSystem;
using MicrosServices.SDK.UserCenter;
using Newtonsoft.Json;
using PermissionSystem.UI.WebSites.Global;
using PermissionSystem.UI.WebSites.Models;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites.Controllers
{
    public class UserController : Controller
    {
        private UserSDK userSDK = new UserSDK();
        private AssignSDK assignSDK = new AssignSDK();

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd()
        {
            return View();
        }
        #endregion 

        #region Basic GET POST
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUcUsersList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<UcUsers> pageResponse = userSDK.GetUserPageList(page, keywords);
            return Json(new PageResponseView<UcUsers>(pageResponse), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UcUsersAdd(UcUsers model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            RegisterPlatformForm registerPlatform = new RegisterPlatformForm()
            {
                UserName = model.UserName,
                UserNo = model.UserNo,
                InputUser = model.InputUser,
                Email = model.Email,
                Phone = model.Phone,
                Password = "123456",
                PlatformNo = AppBusiness.loginModel.PlatformNo
            };
            JsonResponses jsonResponses = userSDK.RegisterPlatfrom(registerPlatform);
            return Json(jsonResponses, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 用户机构分配
        /// <summary>
        /// 用户角色分配页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOrgAssign(string UserNo)
        {
            return View();
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUserOrgAssign(string UserNo)
        {
            UserOrgAssignVo assignVo = new UserOrgAssignVo();
            JsonResponses jsonResponses = assignSDK.GetUserOrgAssign(UserNo);
            if (jsonResponses.ValidateResponses())
            {
                assignVo = JsonConvert.DeserializeObject<UserOrgAssignVo>(JsonConvert.SerializeObject(jsonResponses.data));
            }
            return Json(assignVo, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserOrgsAssignUpdate(UserOrgsForm model)
        {
            JsonResponses responses = assignSDK.CreateUserOrgs(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}