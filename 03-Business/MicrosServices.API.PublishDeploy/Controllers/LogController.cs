using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.DataForm.LogQuery;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.PublishDeploy.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] LogQueryForm logQueryForm)
        {
            try
            {
                List<UcLoginLog> list = DataHandleManager.Instance().UcLoginLogHandle.GetUcLoginLogList(page, logQueryForm).ToList();
                PageResponse<UcLoginLog> response = new PageResponse<UcLoginLog>
                {
                    page = page,
                    dataList = list
                };
                return new JsonResponses(response);
            }
            catch (Exception ex)
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
            UcLoginLog Info = new UcLoginLog();
            Info = DataHandleManager.Instance().UcLoginLogHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }

        #endregion
    }
}