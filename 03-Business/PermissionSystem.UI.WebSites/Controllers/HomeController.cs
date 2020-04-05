using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.VO;
using PermissionSystem.UI.WebSites.Global;
using SkeFramework.Cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RedisHandleManager.Instance().RedisStringHandle.Set("test1", "123");
            return View();
        }

        [HttpGet]
        public JsonResult GetSideBarList()
        {
            MenuSideBar menuSideBar = new MenuSideBar();
            menuSideBar.root = AppBusiness.SideBarList.Where(o => o.ParentNo == ConstData.DefaultNo).OrderBy(o => o.Sort).ToList();
            menuSideBar.child = AppBusiness.SideBarList.Where(o => o.ParentNo != ConstData.DefaultNo).OrderBy(o => o.Sort).ToList();
            return Json(menuSideBar, JsonRequestBehavior.AllowGet);
        }

    }
}