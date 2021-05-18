using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Entities.Constants;
using MicrosServices.Entities.Core.DataForm;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.AdminServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictionaryTypeController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<BsDictionaryType, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.DicType.Contains(keywords) || (o.Descriptions.Contains(keywords)));
            }
            List<BsDictionaryType> list = DataHandleManager.Instance().BsDictionaryTypeHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] QueryBaseFrom query)
        {
            query.InitQuery();
            string QueryNo = "_" + query.queryNo;
            string keywords = query.keywords;
            Expression<Func<BsDictionaryType, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.DicType.Contains(keywords) || (o.Descriptions.Contains(keywords)));
            }
            page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().BsDictionaryTypeHandle.Count(where)));//取记录数
            List<BsDictionaryType> list = DataHandleManager.Instance().BsDictionaryTypeHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
            PageResponse<BsDictionaryType> response = new PageResponse<BsDictionaryType>
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
            BsDictionaryType Info = new BsDictionaryType();
            Info = DataHandleManager.Instance().BsDictionaryTypeHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm]BsDictionaryType model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().BsDictionaryTypeHandle.DictionaryTypeInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm]BsDictionaryType model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().BsDictionaryTypeHandle.DictionaryTypeUpdate(model);
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
            DataHandleManager.Instance().BsDictionaryTypeHandle.CheckDictionaryTypeCanDelete(id);
            ResultCode = DataHandleManager.Instance().BsDictionaryTypeHandle.Delete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetOptionValues(long PlatformNo=ConstData.DefaultNo)
        {
            List<DictionaryOptionValue> optionValues = DataHandleManager.Instance().BsDictionaryTypeHandle.GetOptionValues(PlatformNo);
            return new JsonResponses(optionValues);
        }

        #endregion

    }
}