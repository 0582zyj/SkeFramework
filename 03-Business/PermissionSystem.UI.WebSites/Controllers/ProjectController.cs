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
using System.Web;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites.Controllers
{
    /// <summary>
    /// 项目管理
    /// </summary>
    public class ProjectController : Controller
    {
        private ProjectSdk serverSdk = new ProjectSdk();


        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectAdd()
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
        public JsonResult GetPdProjectInfo(int id)
        {
            PdProject Info = new PdProject();
            JsonResponses responses = serverSdk.GetInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as PdProject;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPdProjectList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PdProject> pageResponse = serverSdk.GetPageList(page, keywords);
            return Json(new PageResponseView<PdProject>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PdProjectAdd(PdProject model)
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
        public JsonResult PdProjectUpdate(PdProject model)
        {
            JsonResponses responses = serverSdk.Update(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PdProjectDelete(int id)
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

        

        /// <summary>
        /// 发布方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ProjectPublish(int id)
        {
            JsonResponses responses = serverSdk.GetInfo(id);
            if (responses.ValidateResponses())
            {
                responses = serverSdk.PublishDeploy(id);
            }
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
    }
}