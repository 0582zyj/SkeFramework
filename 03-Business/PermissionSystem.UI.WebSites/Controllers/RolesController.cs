using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using MicrosServices.SDK.PermissionSystem;
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
    public class RolesController : Controller
    {
        private RolesSDK rolesSDK = new RolesSDK();
        private AssignSDK assignSDK = new AssignSDK();

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RolesList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RolesUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RolesAdd()
        {
            return View();
        }
        #endregion 

        #region Basic GET POST
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPsRolesInfo(int id)
        {
            PsRoles Info = new PsRoles();
            JsonResponses responses = rolesSDK.GetPsRolesInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as PsRoles;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPsRolesList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PsRoles> pageResponse = rolesSDK.GetRolesPageList(page, keywords);
            return Json(new PageResponseView<PsRoles>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsRolesAdd(PsRoles model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = rolesSDK.RolesAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsRolesUpdate(PsRoles model)
        {
            JsonResponses responses = rolesSDK.RolesUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsRolesDelete(int id)
        {
            JsonResponses responses = rolesSDK.RolesDelete(id);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetOptionValues()
        {
            List<OptionValue> optionValues = rolesSDK.GetOptionValues();
            optionValues.Insert(0, OptionValue.Default);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 用户角色页面
        /// <summary>
        /// 用户角色分配页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RolesAssign(long UserNo)
        {
            return View();
        }


        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRolesAssign(long UserNo)
        {
            RolesAssignVo assignVo = new RolesAssignVo();
            JsonResponses jsonResponses = assignSDK.GetRolesAssign(UserNo);
            if (jsonResponses.ValidateResponses())
            {
                assignVo = JsonConvert.DeserializeObject<RolesAssignVo>(JsonConvert.SerializeObject(jsonResponses.data));
            }
            return Json(assignVo, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RolesAssignUpdate(UserRolesForm model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = assignSDK.CreateUserRoles(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}