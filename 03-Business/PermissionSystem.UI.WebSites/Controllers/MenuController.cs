using MicrosServices.Entities.Common;
using MicrosServices.SDK.PermissionSystem;
using Newtonsoft.Json;
using PermissionSystem.UI.WebSites.Global;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites.Controllers
{
    public class MenuController : Controller
    {
        private MenuSdk menuSdk = new MenuSdk();

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuAdd()
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
        public JsonResult GetPsMenuInfo(int id)
        {
            PsMenu Info = new PsMenu();
            JsonResponses responses = menuSdk.GetPsMenuInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info= responses.data as PsMenu;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPsMenuList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PsMenu> pageResponse = menuSdk.GetMenuPageList(page, keywords);
            return Json(pageResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsMenuAdd(PsMenu model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses= menuSdk.MenuAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsMenuUpdate(PsMenu model)
        {
            JsonResponses responses = menuSdk.MenuUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsMenuDelete(int id)
        {
            JsonResponses responses = menuSdk.MenuDelete(id );
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}