using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.Entities.Core.DataForm;
using SkeFramework.Core.ApiCommons.Filter;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.RealTimeSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PushConfigController : ControllerBase
    {
        #region 基础查询
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
            Expression<Func<RtPushconfig, bool>> where = o => o.AppId.Contains(keywords) ;
            page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().RtPushconfigHandle.Count(where)));//取记录数
            List<RtPushconfig> list = DataHandleManager.Instance().RtPushconfigHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
            PageResponse<RtPushconfig> response = new PageResponse<RtPushconfig>
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
            RtPushconfig Info = new RtPushconfig();
            Info = DataHandleManager.Instance().RtPushconfigHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm]RtPushconfig model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().RtPushconfigHandle.PushconfigInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm]RtPushconfig model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().RtPushconfigHandle.PushconfigUpdate(model);
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
            ResultCode = DataHandleManager.Instance().RtPushconfigHandle.Delete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        #endregion

    }
}