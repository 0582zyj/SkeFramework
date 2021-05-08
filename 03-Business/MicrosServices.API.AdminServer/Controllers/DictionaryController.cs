using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Entities.Core.DataForm;
using MicrosServices.Helper.Core.Extends;
using SkeFramework.Core.ApiCommons.Filter;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.AdminServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<BsDictionary, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.DicValue.Contains(keywords) || (o.DicType.Contains(keywords) || o.DicValue.Contains(keywords)));
            }
            List<BsDictionary> list = DataHandleManager.Instance().BsDictionaryHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LoggerFilterAttribute("字典模块", "获取列表信息")]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] QueryBaseFrom query)
        {
            query.InitQuery();
            string QueryNo = "_" + query.queryNo;
            string keywords = query.keywords;
            Expression<Func<BsDictionary, bool>> where = null;
            where = (o => o.DicValue.Contains(keywords) || (o.DicType.Contains(keywords) || o.DicValue.Contains(keywords)));
            page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().BsDictionaryHandle.Count(where)));//取记录数
            List<BsDictionary> list = DataHandleManager.Instance().BsDictionaryHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
            PageResponse<BsDictionary> response = new PageResponse<BsDictionary>
            {
                page = page,
                dataList = list
            };
            return new JsonResponses(response);
        }
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetInfo(int id)
        {
            BsDictionary Info = new BsDictionary();
            Info = DataHandleManager.Instance().BsDictionaryHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm]BsDictionary model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().BsDictionaryHandle.DictionaryInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);

        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm]BsDictionary model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().BsDictionaryHandle.DictionaryUpdate(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Delete([FromForm]int id)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().BsDictionaryHandle.Delete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetOptionValues(string Code)
        {
            List<DictionaryOptionValue> optionValues = DataHandleManager.Instance().BsDictionaryHandle.GetOptionValues(Code);
            return new JsonResponses(optionValues);
        }

        #endregion

    }
}