using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using MicrosServices.SDK.AdminSystem;
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
    public class DictionaryTypeController : Controller
    {
        private DictionaryTypeSDK dictionaryTypeSDK = new DictionaryTypeSDK();
        #region 基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryTypeList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryTypeUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryTypeAdd()
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
        public JsonResult GetBsDictionaryTypeInfo(int id)
        {
            BsDictionaryType Info = new BsDictionaryType();
            JsonResponses responses = dictionaryTypeSDK.GetInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.GetDataValue<BsDictionaryType>();
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBsDictionaryTypeList(int curPage = 1, string keywords = "", long DictionaryNo = -1)
        {
            PageModel page = new PageModel(curPage);
            PageResponse<BsDictionaryType> pageResponse = dictionaryTypeSDK.GetPageList(page, keywords, DictionaryNo);
            return Json(new PageResponseView<BsDictionaryType>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BsDictionaryTypeAdd(BsDictionaryType model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            model.UpdateUser = model.InputUser;
            JsonResponses responses = dictionaryTypeSDK.DictionaryTypeAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BsDictionaryTypeUpdate(BsDictionaryType model)
        {
            model.UpdateUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = dictionaryTypeSDK.DictionaryTypeUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BsDictionaryTypeDelete(int id)
        {
            JsonResponses responses = dictionaryTypeSDK.DictionaryDelete(id);
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
            long PlatformNo = LoginModel.Instance().PlatformNo;
            List<DictionaryOptionValue> optionValues = dictionaryTypeSDK.GetDictionaryOptionValues(LoginModel.Instance().PlatformNo);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }
    }
}