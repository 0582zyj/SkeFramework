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
    public class MessageController : ControllerBase
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
            Expression<Func<RtMessage, bool>> where = o => o.AppId.Contains(keywords);
            page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().RtMessageHandle.Count(where)));//取记录数
            List<RtMessage> list = DataHandleManager.Instance().RtMessageHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
            PageResponse<RtMessage> response = new PageResponse<RtMessage>
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
            RtMessage Info = new RtMessage();
            Info = DataHandleManager.Instance().RtMessageHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm]RtMessage model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().RtMessageHandle.RtMessageInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm]RtMessage model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().RtMessageHandle.RtMessageUpdate(model);
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
            ResultCode = DataHandleManager.Instance().RtMessageHandle.Delete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        #endregion

    }
}