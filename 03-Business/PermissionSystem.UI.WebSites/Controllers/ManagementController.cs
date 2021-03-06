﻿using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.Extends;
using MicrosServices.Helper.Core.Form;
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
    public class ManagementController : Controller
    {
        private ManagementSDK managementSDK = new ManagementSDK();
        private RolesSDK rolesSDK = new RolesSDK();
        private AssignSDK assignSDK = new AssignSDK();
        private TreeSDK treeSDK = new TreeSDK();

        #region 基础页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementList()
        {
            return View();
        }
        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementUpdate(int id)
        {
            return View();
        }
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementAdd()
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
        public JsonResult GetPsManagementInfo(int id)
        {
            PsManagement Info = new PsManagement();
            JsonResponses responses = managementSDK.GetPsManagementInfo(id);
            if (responses.code == JsonResponses.SuccessCode)
            {
                Info = responses.data as PsManagement;
            }
            return Json(Info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPsManagementList(int curPage = 1, string keywords = "",long ManagementNo=-1)
        {
            PageModel page = new PageModel(curPage);
            PageResponse<PsManagement> pageResponse = managementSDK.GetManagementPageList(page, keywords, ManagementNo);
            return Json(new PageResponseView<PsManagement>(pageResponse), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsManagementAdd(PsManagement model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = managementSDK.ManagementAdd(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsManagementUpdate(PsManagement model)
        {
            JsonResponses responses = managementSDK.ManagementUpdate(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PsManagementDelete(int id)
        {
            JsonResponses responses = managementSDK.ManagementDelete(id);
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
            List<OptionValue> optionValues = managementSDK.GetOptionValues();
            optionValues.Insert(0, OptionValue.Default);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetManagementOptionValues( long ManagementType)
        {
            long PlatformNo = AppBusiness.loginModel.PlatformNo;
            List<ManagementOptionValue> optionValues = managementSDK.GetManagementOptionValues(PlatformNo,ManagementType);
            optionValues.Insert(0, ManagementOptionValue.Default);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }
        
        #region 角色权限页面
        /// <summary>
        /// 权限分配
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementAssign(long RolesNo)
        {
            return View();
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetManagementAssign(long RolesNo)
        {
            ManagmentAssignVo assignVo = new ManagmentAssignVo();
            JsonResponses jsonResponses= assignSDK.GetManagementAssign(RolesNo,(int)ManagementType.MENU_TYPE);
            if(jsonResponses.ValidateResponses()) {
                assignVo = JsonConvert.DeserializeObject<ManagmentAssignVo>(JsonConvert.SerializeObject( jsonResponses.data));
            }
            return Json(assignVo, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ManagementAssignUpdate(ManagementRolesForm model)
        {
            model.InputUser = AppBusiness.loginModel.UserNo;
            JsonResponses responses = assignSDK.CreateManagementRoles(model);
            return Json(responses, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region
        /// <summary>
        /// 获取菜单权限值
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUserManagementList()
        {
            string UserNo = AppBusiness.loginModel.UserNo;
            List<ManagementOptionValue > optionValues = managementSDK.GetUserManagementList(UserNo);
            return Json(optionValues, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 给页面提供json格式的节点数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetManagementTreeList()
        {
            List<TreeNodeInfo> treeNodes = treeSDK.GetManagementTreeList(AppBusiness.loginModel.PlatformNo);
            //将获取的节点集合转换为json格式字符串，并返回
            return JsonConvert.SerializeObject(treeNodes);
        }
        #endregion
    }
}