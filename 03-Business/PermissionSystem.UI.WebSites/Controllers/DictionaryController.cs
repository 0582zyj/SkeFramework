using MicrosServices.Entities.Common.BaseSystem;
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
    public class DictionaryController : Controller
    {
        private DictionarySDK dictionarySDK = new DictionarySDK();
        #region 基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryAdd()
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
        public JsonResult GetBsDictionaryInfo(int id)
        {
            BsDictionary Info = new BsDictionary();
            JsonResponses responses = dictionarySDK.GetInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as BsDictionary;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBsDictionaryList(int curPage = 1, string keywords = "", long DictionaryNo = -1)
        {
            PageModel page = new PageModel(curPage);
            PageResponse<BsDictionary> pageResponse = dictionarySDK.GetPageList(page, keywords, DictionaryNo);
            return Json(new PageResponseView<BsDictionary>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BsDictionaryAdd(BsDictionary model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            model.UpdateUser = model.InputUser;
           JsonResponses responses = dictionarySDK.DictionaryAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BsDictionaryUpdate(BsDictionary model)
        {
            model.UpdateUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = dictionarySDK.DictionaryUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BsDictionaryDelete(int id)
        {
            JsonResponses responses = dictionarySDK.DictionaryDelete(id);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 字典键值对
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetOptionValues(string dicType)
        {
            List<DictionaryOptionValue> optionValues = dictionarySDK.GetOptionValues(dicType);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}