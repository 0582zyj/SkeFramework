using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.SDK.RealTimeSystem;
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
    public class PushConfigController : Controller
    {
        private PushConfigSDK pushConfigSDK = new PushConfigSDK();
        #region 基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PushConfigList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PushConfigUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PushConfigAdd()
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
        public JsonResult GetRtPushconfigInfo(int id)
        {
            RtPushconfig Info = new RtPushconfig();
            JsonResponses responses = pushConfigSDK.GetInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.GetDataValue< RtPushconfig>();
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRtPushconfigList(int curPage = 1, string keywords = "", long DictionaryNo = -1)
        {
            PageModel page = new PageModel(curPage);
            PageResponse<RtPushconfig> pageResponse = pushConfigSDK.GetPageList(page, keywords, DictionaryNo);
            return Json(new PageResponseView<RtPushconfig>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RtPushconfigAdd(RtPushconfig model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            model.UpdateUser = model.InputUser;
            JsonResponses responses = pushConfigSDK.PushconfigAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RtPushconfigUpdate(RtPushconfig model)
        {
            model.UpdateUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = pushConfigSDK.PushconfigUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RtPushconfigDelete(int id)
        {
            JsonResponses responses = pushConfigSDK.PushconfigDelete(id);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}