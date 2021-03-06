﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
using MicrosServices.Entities.Core.DataForm;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords="")
        {
            Expression<Func<PsMenu, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] QueryBaseFrom query)
        {
            try
            {
                query.InitQuery();
                string QueryNo = "_" + query.queryNo;
                string keywords = query.keywords;
                Expression<Func<PsMenu, bool>> where = null;
                where = (o => o.Name.Contains(keywords) && (o.TreeLevelNo.Contains(QueryNo)||o.MenuNo==query.queryNo));
                page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().PsMenuHandle.Count(where)));//取记录数
                List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PsMenu> response = new PageResponse<PsMenu>
                {
                    page = page,
                    dataList = list
                };
                return new JsonResponses(response);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetInfo(int id)
        {
            PsMenu Info = new PsMenu();
            Info = DataHandleManager.Instance().PsMenuHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm] PsMenu menu)
        {
            try
            {
                bool checkResult = true;
                checkResult= DataHandleManager.Instance().PsMenuHandle.CheckNameIsExist(menu.MenuNo, menu.Name);
                if (checkResult)
                {
                    return new JsonResponses(JsonResponses.FailedCode, ErrorResultType.ERROR_MENU_NAME_REPEAT.ToString());
                }
                checkResult = DataHandleManager.Instance().PsMenuHandle.CheckMenuNoIsExist(menu.ParentNo);
                if (!checkResult)
                {
                    return new JsonResponses(JsonResponses.FailedCode, ErrorResultType.ERROR_MENU_PARENTNO_NOT_EXISET.ToString());
                }
                int result= DataHandleManager.Instance().PsMenuHandle.MenuInsert(menu);
                if (result > 0)
                {
                    return JsonResponses.Success;
                }
                return JsonResponses.Failed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Delete([FromForm] int id)
        {
            int result = DataHandleManager.Instance().PsMenuHandle.Delete(id);
            if (result > 0)
            {
                return JsonResponses.Success;
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm] PsMenu menu)
        {
            try
            {
                bool checkResult = true;
                checkResult = DataHandleManager.Instance().PsMenuHandle.CheckNameIsExist(menu.MenuNo, menu.Name);
                if (checkResult)
                {
                    return new JsonResponses(JsonResponses.FailedCode, ErrorResultType.ERROR_MENU_NAME_REPEAT.ToString());
                }
                if (menu.ParentNo != -1)
                {
                    checkResult = DataHandleManager.Instance().PsMenuHandle.CheckMenuNoIsExist(menu.ParentNo);
                    if (!checkResult)
                    {
                        return new JsonResponses(JsonResponses.FailedCode, ErrorResultType.ERROR_MENU_PARENTNO_NOT_EXISET.ToString());
                    }
                }
                int result = DataHandleManager.Instance().PsMenuHandle.MenuUpdate(menu);
                if (result > 0)
                {
                    return JsonResponses.Success;
                }
                return JsonResponses.Failed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetOptionValues()
        {
            List<OptionValue> optionValues = DataHandleManager.Instance().PsMenuHandle.GetOptionValues();
            return new JsonResponses(optionValues);
        }
        #endregion

        #region 用户菜单
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetUserMenusList(string UserNo)
        {
            List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetUserMenusList(UserNo).ToList();
            return new JsonResponses(list);
        }
        #endregion

    }
}