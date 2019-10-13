using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementList()
        {
            List<PsManagement> managements = DataHandleManager.Instance().PsManagementHandle.GetList().ToList();
            return new JsonResponses(managements);
        }

        [HttpGet]
        public ActionResult<JsonResponses> GetManagementPageList(int pageIndex , int pageSize = 10, string keywords = "")
        {
            Expression<Func<PsManagement, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains( keywords));
            }
            int total = Convert.ToInt32(DataHandleManager.Instance().PsManagementHandle.Count(where));//取记录数
            if (pageIndex == 0)
            {
                pageSize = total> pageSize ? total: pageSize;
            }
            PageModel page = new PageModel(pageIndex, pageSize, total);
            List<PsManagement> managements = DataHandleManager.Instance().PsManagementHandle
                .GetDefaultPagedList(pageIndex, pageSize,where).ToList();
            var obj = new { pages = page, dataList = managements };
            return new JsonResponses(obj);

        }


    }
}
