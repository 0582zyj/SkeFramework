using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.SDK.RealTimeSystem;
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
    public class MessageController : Controller
    {

        private MessageSDK messageSDK = new MessageSDK();
        private EmailSDK emailSDK = new EmailSDK();
        private ShortMessageSDK shortMessageSDK = new ShortMessageSDK();

        #region 基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageList()
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
        public JsonResult GetRtMessageInfo(int id)
        {
            RtMessage Info = new RtMessage();
            JsonResponses responses = messageSDK.GetInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.GetDataValue< RtMessage>();
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRtMessageList(int curPage = 1, string keywords = "", long MessageId = -1)
        {
            PageModel page = new PageModel(curPage);
            PageResponse<RtMessage> pageResponse = messageSDK.GetPageList(page, keywords, MessageId);
            return Json(new PageResponseView<RtMessage>(pageResponse), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 邮件基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailList()
        {
            return View();
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRtEmailList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<RtEmail> pageResponse = emailSDK.GetPageList(page, keywords);
            return Json(new PageResponseView<RtEmail>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 短信基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortMessageList()
        {
            return View();
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRtShortMessageList(int curPage = 1, string keywords = "")
        {
            PageModel page = new PageModel(curPage);
            PageResponse<RtShortMessage> pageResponse = shortMessageSDK.GetPageList(page);
            return Json(new PageResponseView<RtShortMessage>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        #endregion 
    }
}