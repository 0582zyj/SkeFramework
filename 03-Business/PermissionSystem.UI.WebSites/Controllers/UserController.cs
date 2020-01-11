using MicrosServices.Entities.Common;
using MicrosServices.SDK.UserCenter;
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

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
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
        public JsonResult GetUcUserList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<UcUsers> pageResponse = userSDK.GetUserPageList(page, keywords);
            return Json(new PageResponseView<UcUsers>(pageResponse), JsonRequestBehavior.AllowGet);
        }
     

        #endregion

    }
}