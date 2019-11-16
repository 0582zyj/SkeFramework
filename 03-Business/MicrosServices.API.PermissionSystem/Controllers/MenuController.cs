using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

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
            Expression<Func<PsMenu, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            int total = Convert.ToInt32(DataHandleManager.Instance().PsMenuHandle.Count(where));//取记录数
            List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetDefaultPagedList(page.PageIndex, page.PageSize,where).ToList();
            PageResponse<PsMenu> response = new PageResponse<PsMenu>();
            response.page = page;
            response.dataList = list;
            return new JsonResponses(response);
        }
    }
}