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
        public ActionResult<IEnumerable<PsManagement>> GetManagementPageList(int pageIndex , int pageSize = 10, string keywords = "")
        {
            return null;
            //Expression<Func<PsManagement, bool>> where = null;
            //if (!String.IsNullOrEmpty(keywords))
            //{
            //    where  = (o => o.id == Id);
            //}
            //List<PsManagement> managements = DataHandleManager.Instance().PsManagementHandle
            //    .GetDefaultPagedList(pageIndex, pageSize).ToList();

            //var total = DataHandleManager.Instance().PsManagementHandle.Count();//取记录数
            //var pages = new PageDTO(curPage, Convert.ToInt32(total));//初始化分页类
            //if (curPage == 0)
            //{
            //    pages.pagesize = Convert.ToInt32(total);
            //}
            //List<PsManagement> list = DataHandleManager.Instance().PsManagementHandle.GetDefaultPagedList(curPage, pages.pagesize).ToList();
            //var obj = new { pages = pages, dataList = list };//构造对象
            //return Json(obj, JsonRequestBehavior.AllowGet);

        }


    }
}
