using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.SDK.PermissionSystem;
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
    public class ManagementController : Controller
    {
        private ManagementSDK managementSDK = new ManagementSDK();

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementAdd()
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
        public JsonResult GetPsManagementInfo(int id)
        {
            PsManagement Info = new PsManagement();
            JsonResponses responses = managementSDK.GetPsManagementInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as PsManagement;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPsManagementList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PsManagement> pageResponse = managementSDK.GetManagementPageList(page, keywords);
            return Json(new PageResponseView<PsManagement>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsManagementAdd(PsManagement model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = managementSDK.ManagementAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsManagementUpdate(PsManagement model)
        {
            JsonResponses responses = managementSDK.ManagementUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsManagementDelete(int id)
        {
            JsonResponses responses = managementSDK.ManagementDelete(id);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetOptionValues()
        {
            List<OptionValue> optionValues = managementSDK.GetOptionValues();
            optionValues.Insert(0, OptionValue.Default);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }
    }
}