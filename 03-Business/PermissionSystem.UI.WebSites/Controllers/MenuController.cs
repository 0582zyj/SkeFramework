using MicrosServices.Entities.Common;
using MicrosServices.SDK.PermissionSystem;
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
        public ActionResult PsMenuUpdate(int id)
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
        ///// <summary>
        ///// 根据主键ID获取信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetPsMenuInfo(int id)
        //{
        //    PsMenu Info = new PsMenu();
        //    Info = DataHandleManager.Instance().PsMenuHandle.GetModelByKey(id.ToString());
        //    return Json(Info, JsonRequestBehavior.AllowGet);
        //}
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
            JsonResponses responses= menuSdk.MenuAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsMenuAdd1(string Name)
        {
            return Json(Name+"test", JsonRequestBehavior.AllowGet);
        }
        ///// <summary>
        ///// 更新提交方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult PsMenuUpdate(PsMenu model)
        //{
        //    var ResultCode = -1;
        //    ResultCode = DataHandleManager.Instance().PsMenuHandle.Update(model);
        //    return Json(GetResultMsg(ResultCode > 0 ? 200 : 201), JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// 删除提交方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult PsMenuDelete(int id)
        //{
        //    var ResultCode = -1;
        //    ResultCode = DataHandleManager.Instance().PsMenuHandle.Delete(id);
        //    return Json(GetResultMsg(ResultCode > 0 ? 300 : 301), JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// 根据状态获取结果
        ///// </summary>
        ///// <returns></returns>
        //public Object GetResultMsg(int ResultCode)
        //{
        //    string Msg = "操作成功";
        //    switch (ResultCode)
        //    {
        //        case -1:
        //            Msg = "服务器响应错误";
        //            break;
        //        case 100:
        //            Msg = "新增成功";
        //            break;
        //        case 101:
        //            Msg = "新增失败";
        //            break;
        //        case 200:
        //            Msg = "更新成功";
        //            break;
        //        case 201:
        //            Msg = "更新失败";
        //            break;
        //        case 300:
        //            Msg = "删除成功";
        //            break;
        //        case 301:
        //            Msg = "删除失败";
        //            break;
        //        default:
        //            Msg = "未知错误";
        //            break;
        //    }
        //    var obj = new { ResultCode = ResultCode, ResultMsg = Msg };//构造对象
        //    return obj;
        //}
        #endregion
    }
}