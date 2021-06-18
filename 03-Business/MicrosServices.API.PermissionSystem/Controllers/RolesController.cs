using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.DataForm;
using MicrosServices.Helper.Core;
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
    public class RolesController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<PsRoles, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            List<PsRoles> list = DataHandleManager.Instance().PsRolesHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] QueryBaseFrom query)
        {
            try
            {
                query.InitQuery();
                string QueryNo = "_" + query.queryNo;
                string keywords = query.keywords;
                Expression<Func<PsRoles, bool>> where = (o => o.Name.Contains(keywords)
                && (o.TreeLevelNo.Contains(QueryNo) || o.RolesNo == query.queryNo));
                page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().PsRolesHandle.Count(where)));//取记录数
                List<PsRoles> list = DataHandleManager.Instance().PsRolesHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PsRoles> response = new PageResponse<PsRoles>
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
            PsRoles Info = new PsRoles();
            Info = DataHandleManager.Instance().PsRolesHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 新增提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm]PsRoles model)
        {
            var ResultCode = -1;
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNameIsExist(model.Name);
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ParentNo);
            ResultCode = DataHandleManager.Instance().PsRolesHandle.RolesInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);

        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm]PsRoles model)
        {
            var ResultCode = -1;
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNameIsExist(model.Name, model.ManagementNo);
            //DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ManagementNo);
            ResultCode = DataHandleManager.Instance().PsRolesHandle.RolesUpdate(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Delete([FromForm] int id)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsRolesHandle.RolesDelete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ManagementNos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> BatchDelete([FromBody]long[] RolesNos)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsRolesHandle.BatchRolesDelete(RolesNos);
            return (ResultCode == RolesNos.Length ? JsonResponses.Success : JsonResponses.Failed);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetOptionValues()
        {
            List<OptionValue> optionValues = DataHandleManager.Instance().PsRolesHandle.GetOptionValues();
            return new JsonResponses(optionValues);
        }

        #endregion
        
    }
}
