using MicrosServices.Entities.Common;
using MicrosServices.SDK.LogSystem;
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
    public class LogController : Controller
    {
        private PublishLogSDK publishLogSDK = new PublishLogSDK();
        private LoginLogSDK loginLogSDK = new LoginLogSDK();

        #region 发布日志
        /// <summary>
        /// 发布日志列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishLogList()
        {
            return View();
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPublishLogList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<UcLoginLog> pageResponse = publishLogSDK.GetPublishLogPageList(page, keywords);
            return Json(new PageResponseView<UcLoginLog>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 登录日志
        /// <summary>
        /// 登录日志列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginLogList()
        {
            return View();
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLoginLogList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<UcLoginLog> pageResponse = loginLogSDK.GetLoginLogPageList(page, keywords);
            return Json(new PageResponseView<UcLoginLog>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}