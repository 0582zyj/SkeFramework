using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.UserCenter.FORM;
using MicrosServices.SDK.PermissionSystem;
using MicrosServices.SDK.UserCenter;
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
    public class PlatformController : Controller
    {
        private PlatformSdk platformSdk = new PlatformSdk();
        private UserSDK userSDK = new UserSDK();

        #region 页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PlatformList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PlatformUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PlatformAdd()
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
        public JsonResult GetPsPlatformInfo(int id)
        {
            PsPlatform Info = new PsPlatform();
            JsonResponses responses = platformSdk.GetPsPlatformInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as PsPlatform;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
           
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPsPlatformList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PsPlatform> pageResponse = platformSdk.GetPlatformPageList(page, keywords);
            return Json(new PageResponseView<PsPlatform>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsPlatformAdd(PsPlatform model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            RegisterPlatformForm registerPlatform = new RegisterPlatformForm()
            {
                UserName = model.DefaultUserName,
                UserNo = model.DefaultUserNo,
                InputUser = model.InputUser,
                Email = "",
                Phone = "",
                Password = "123456",
                PlatformNo=model.PlatformNo
            };
            JsonResponses jsonResponses = userSDK.RegisterPlatfrom(registerPlatform);
            if (jsonResponses.code == JsonResponses.SuccessCode)
            {
                RegisterPlatformForm registerResult = JsonConvert.DeserializeObject<RegisterPlatformForm>(JsonConvert.SerializeObject(jsonResponses.data));
                model.DefaultUserNo = registerResult.UserNo;
                JsonResponses responses = platformSdk.PlatformAdd(model);
                return Json(responses, JsonRequestBehavior.AllowGet);
            }
            return Json(jsonResponses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsPlatformUpdate(PsPlatform model)
        {
            JsonResponses responses = platformSdk.PlatformUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsPlatformDelete(int id)
        {
            JsonResponses responses = platformSdk.GetPsPlatformInfo(id);
            if (responses.ValidateResponses())
            {
                PsPlatform platform= responses.data as PsPlatform;
                responses = userSDK.CancelPlatform(platform.DefaultUserNo);
                if (responses.ValidateResponses())
                {
                    responses = platformSdk.PlatformDelete(id);
                }
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
            long PlatformNo = LoginModel.Instance().PlatformNo;
            List<OptionValue> optionValues = platformSdk.GetOptionValues(LoginModel.Instance().PlatformNo);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }


    }
}
