using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.API.PermissionSystem.Controllers.BaseControllers;
using MicrosServices.API.PermissionSystem.Filters;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.DataForm;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizationController : SkeControllers
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<PsOrganization, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            List<PsOrganization> list = DataHandleManager.Instance().PsOrganizationHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeFilterAttribute(1)]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] QueryBaseFrom query)
        {
            try
            {
                List<long> currentPlatformNos = this.GetCurrentUserPlatfromNos();
                query.InitQuery();
                string QueryNo = "_" + query.queryNo;
                string keywords = query.keywords;
                Expression<Func<PsOrganization, bool>> where = null;
                where = o => o.Name.Contains(keywords) && (o.TreeLevelNo.Contains(QueryNo) || o.OrgNo == query.queryNo)
                   && currentPlatformNos.Contains(o.PlatformNo);
                page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().PsOrganizationHandle.Count(where)));//取记录数
                List<PsOrganization> list = DataHandleManager.Instance().PsOrganizationHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PsOrganization> response = new PageResponse<PsOrganization>
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
            PsOrganization Info = new PsOrganization();
            Info = DataHandleManager.Instance().PsOrganizationHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "role.add")]
        public ActionResult<JsonResponses> Create([FromForm]PsOrganization model)
        {
            var ResultCode = -1;
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNameIsExist(model.Name);
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ParentNo);
            ResultCode = DataHandleManager.Instance().PsOrganizationHandle.OrganizationInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);

        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "role.update")]
        public ActionResult<JsonResponses> Update([FromForm]PsOrganization model)
        {
            var ResultCode = -1;
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNameIsExist(model.Name, model.ManagementNo);
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ManagementNo);
            ResultCode = DataHandleManager.Instance().PsOrganizationHandle.OrganizationUpdate(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "role.delete")]
        public ActionResult<JsonResponses> Delete([FromForm] int id)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsOrganizationHandle.OrganizationDelete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ManagementNos"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(1)]
        public ActionResult<JsonResponses> BatchDelete([FromBody]long[] RolesNos)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsOrganizationHandle.BatchDelete(RolesNos);
            return (ResultCode == RolesNos.Length ? JsonResponses.Success : JsonResponses.Failed);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeFilterAttribute(1)]
        public ActionResult<JsonResponses> GetOptionValues()
        {
            List<long> currentPlatformNos = this.GetCurrentUserPlatfromNos();
            List<OptionValue> optionValues = DataHandleManager.Instance().PsOrganizationHandle.GetOptionValues(currentPlatformNos);
            return new JsonResponses(optionValues);
        }
        #endregion

        
    }
}
