using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.Extends;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<PsManagement, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            List<PsManagement> list = DataHandleManager.Instance().PsManagementHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] string keywords = "")
        {
            try
            {
                Expression<Func<PsManagement, bool>> where = null;
                if (!String.IsNullOrEmpty(keywords))
                {
                    where = (o => o.Name.Contains(keywords));
                }
                page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().PsManagementHandle.Count(where)));//取记录数
                List<PsManagement> list = DataHandleManager.Instance().PsManagementHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PsManagement> response = new PageResponse<PsManagement>
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
            PsManagement Info = new PsManagement();
            Info = DataHandleManager.Instance().PsManagementHandle.GetModelByKey(id.ToString());
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
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNameIsExist(model.Name);
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ParentNo);
            ResultCode = DataHandleManager.Instance().PsManagementHandle.ManagementInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);

        }
        /// <summary>
        /// 更新提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm]PsManagement model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNameIsExist(model.Name, model.ManagementNo);
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ParentNo);
            ResultCode = DataHandleManager.Instance().PsManagementHandle.ManagementUpdate(model);
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
            ResultCode = DataHandleManager.Instance().PsManagementHandle.Delete(id);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ManagementNos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> BatchDelete([FromBody]long[] ManagementNos)
        {
            var ResultCode = -1;
            ResultCode = DataHandleManager.Instance().PsManagementHandle.BatchDelete(ManagementNos);
            return (ResultCode == ManagementNos.Length ? JsonResponses.Success : JsonResponses.Failed);
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
            List<OptionValue> optionValues = DataHandleManager.Instance().PsManagementHandle.GetOptionValues();
            return new JsonResponses(optionValues);
        }

        /// <summary>
        /// 获取权限键值对列表
        /// </summary>
        /// <param name="PlatformNo">平台号</param>
        /// <param name="ManagementType">权限类型</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementOptionValues([FromQuery]long PlatformNo, [FromQuery] long ManagementType)
        {
            List<ManagementOptionValue> optionValues = DataHandleManager.Instance().PsManagementHandle.GetManagementOptions(PlatformNo, ManagementType);
            return new JsonResponses(optionValues);
        }
        #endregion

        #region 菜单权限列表
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetMenuManagementOptions([FromQuery]long MenuNo)
        {
            bool result = DataHandleManager.Instance().PsMenuHandle.CheckMenuNoIsExist(MenuNo);
            if (!result)
            {
                return new JsonResponses(JsonResponses.FailedCode, ErrorResultType.ERROR_MENUNO_NOT_EXISET.ToString());
            }
            List<long> MenuNos = new List<long>() { MenuNo };
            List<ManagementOptionValue> optionValues = DataHandleManager.Instance().PsMenuManagementHandle.GetManagementOptionValues(MenuNos, (int)ManagementType.OPERATE_TYPE);
            return new JsonResponses(optionValues);
        }
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetUserManagementList([FromQuery]string UserNo)
        {
            List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetUserMenusList(UserNo).ToList();
            List<long> MenuNos = list.Select(o => o.MenuNo).ToList();
            List<ManagementOptionValue> optionValues = DataHandleManager.Instance().PsMenuManagementHandle.GetManagementOptionValues(MenuNos, (int)ManagementType.OPERATE_TYPE);
            return new JsonResponses(optionValues);
        }
        #endregion
    }
}
