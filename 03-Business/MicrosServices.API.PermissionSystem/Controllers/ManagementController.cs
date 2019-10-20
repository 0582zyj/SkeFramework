using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Responses;
using SkeFramework.Core.ApiCommons.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取全部权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementList()
        {
            List<PsManagement> managements = DataHandleManager.Instance().PsManagementHandle.GetList().ToList();
            return new JsonResponses(managements);
        }
        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">每页多少行</param>
        /// <param name="keywords">权限名称</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementPageList(int pageIndex, int pageSize = PageModel.DefaultPageSize, string keywords = "")
        {
            Expression<Func<PsManagement, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            int total = Convert.ToInt32(DataHandleManager.Instance().PsManagementHandle.Count(where));//取记录数
            PageModel page = new PageModel(pageIndex, pageSize, total);
            List<PsManagement> managements = DataHandleManager.Instance().PsManagementHandle
                .GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
            var obj = new { pages = page, dataList = managements };
            return new JsonResponses(obj);
        }
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementInfo(int id)
        {
            PsManagement Info = DataHandleManager.Instance().PsManagementHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm]PsManagement model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsManagementHandle.ManagementInser(model);
            if (ResultCode > 0) throw new Exception("ResultCode");
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);

        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update(PsManagement model)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsManagementHandle.Update(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Delete(int id)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsManagementHandle.Delete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ManagementNos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> BatchDelete(long[] ManagementNos)
        {
            return null;
        }
        #endregion

    }
}
