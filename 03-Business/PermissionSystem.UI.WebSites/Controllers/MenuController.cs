using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.Form.AssignForm;
using MicrosServices.Helper.Core.VO;
using MicrosServices.Helper.Core.VO.AssignVo;
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
    public class MenuController : Controller
    {
        private MenuSdk menuSdk = new MenuSdk();
        private AssignSDK assignSDK = new AssignSDK();
        private TreeSDK treeSDK = new TreeSDK();

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
                Info = responses.data as PsMenu;
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
            return Json(new PageResponseView<PsMenu>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsMenuAdd(PsMenu model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = menuSdk.MenuAdd(model);
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
            JsonResponses responses = menuSdk.MenuDelete(id);
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
            List<OptionValue> optionValues = menuSdk.GetOptionValues();
            optionValues.Insert(0, OptionValue.Default);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }

        #region 权限菜单页面
        /// <summary>
        /// 权限菜单分配
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuAssign(long ManagementNo)
        {
            return View();
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMenuAssign(long ManagementNo)
        {
            MenuAssignVo assignVo = new MenuAssignVo();
            JsonResponses jsonResponses = assignSDK.GetMenuAssign(ManagementNo);
            if (jsonResponses.ValidateResponses())
            {
                assignVo = JsonConvert.DeserializeObject<MenuAssignVo>(JsonConvert.SerializeObject(jsonResponses.data));
            }
            return Json(assignVo, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MenuAssignUpdate(ManagementMenusForm model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = assignSDK.CreateManagementMenus(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 菜单权限页面
        /// <summary>
        /// 菜单权限分配
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuManagementAssign(long MenuNo)
        {
            return View();
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMenuManagmentAssign(long MenuNo)
        {
            MenuManagmentAssignVo assignVo = new MenuManagmentAssignVo();
            JsonResponses jsonResponses = assignSDK.GetMenuManagmentAssign(MenuNo);
            if (jsonResponses.ValidateResponses())
            {
                assignVo = JsonConvert.DeserializeObject<MenuManagmentAssignVo>(JsonConvert.SerializeObject(jsonResponses.data));
            }
            return Json(assignVo, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MenuManagementsUpdate(MenuManagementsForm model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = assignSDK.CreateMenuManagements(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        #endregion


        /// <summary>
        /// 给页面提供json格式的节点数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetMenuTreeList()
        {
            List<TreeNodeInfo> treeNodes = treeSDK.GetMenuTreeList(AppBusiness.loginModel.PlatformNo);
            //将获取的节点集合转换为json格式字符串，并返回
            return JsonConvert.SerializeObject(treeNodes);
        }
    }
   

 
}