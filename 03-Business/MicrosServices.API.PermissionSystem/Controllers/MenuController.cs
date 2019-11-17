﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Constants;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

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
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] string keywords = "")
        {
            try
            {
                Expression<Func<PsMenu, bool>> where = null;
                if (!String.IsNullOrEmpty(keywords))
                {
                    where = (o => o.Name.Contains(keywords));
                }
                int total = Convert.ToInt32(DataHandleManager.Instance().PsMenuHandle.Count(where));//取记录数
                List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PsMenu> response = new PageResponse<PsMenu>();
                response.page = page;
                response.dataList = list;
                return new JsonResponses(response);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Add([FromForm] PsMenu menu)
        {
            try
            {
                bool checkResult = true;
                checkResult= DataHandleManager.Instance().PsMenuHandle.CheckNameIsExist(menu.MenuNo, menu.Name);
                if (!checkResult)
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
                menu.InputTime = DateTime.Now;
                menu.MenuNo = AutoIDWorker.Example.GetAutoID();
                int result= DataHandleManager.Instance().PsMenuHandle.Insert(menu);
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
    }
}