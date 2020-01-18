using MicrosServices.Helper.Core.VO;
using PermissionSystem.UI.WebSites.Global;
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
            return View();
        }

        [HttpGet]
        public JsonResult GetSideBarList()
        {
            MenuSideBar menuSideBar = new MenuSideBar();
            menuSideBar.root = AppBusiness.SideBarList;
            menuSideBar.child = AppBusiness.SideBarList;
            return Json(menuSideBar, JsonRequestBehavior.AllowGet);
        }

    }
}