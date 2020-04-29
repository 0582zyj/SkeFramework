using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using MicrosServices.SDK.PublishDeploy;
using PermissionSystem.UI.WebSites.Global;
using PermissionSystem.UI.WebSites.Models;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites.Controllers
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class ServerController : Controller
    {
        private ServerSdk serverSdk = new ServerSdk();
    

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerAdd()
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
        public JsonResult GetPdServerInfo(int id)
        {
            PdServer Info = new PdServer();
            JsonResponses responses = serverSdk.GetInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as PdServer;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPdServerList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PdServer> pageResponse = serverSdk.GetPageList(page, keywords);
            return Json(new PageResponseView<PdServer>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PdServerAdd(PdServer model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = serverSdk.Add(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PdServerUpdate(PdServer model)
        {
            JsonResponses responses = serverSdk.Update(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PdServerDelete(int id)
        {
            JsonResponses responses = serverSdk.GetInfo(id);
            if (responses.ValidateResponses())
            {
                responses = serverSdk.Delete(id);
            }
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
            List<OptionValue> optionValues = serverSdk.GetOptionValues();
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }


    }
}