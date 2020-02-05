using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssignController : ControllerBase
    {
        #region 用户角色授权
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> CreateUserRoles([FromBody]UserRolesForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().UcUsersHandle.CheckUserNoIsExist(model.UserNo);
            if (model.RolesNos != null)
            {
                foreach (var nos in model.RolesNos)
                {
                    DataHandleManager.Instance().PsRolesHandle.CheckRolesNoIsExist(nos);
                }
            }
            ResultCode = DataHandleManager.Instance().PsUserRolesHandle.UserRolesInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetRolesAssign([FromQuery]long UserNo)
        {
            RolesAssignVo assignVo = DataHandleManager.Instance().PsUserRolesHandle.GetRolesAssign(UserNo);
            return new JsonResponses(assignVo);
        }
        #endregion

        #region 用户机构授权
        /// <summary>
        /// 机构授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> CreateUserOrgs([FromBody]UserOrgsForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().UcUsersHandle.CheckUserNoIsExist(model.UserNo);
            if (model.OrgNos != null)
            {
                foreach (var nos in model.OrgNos)
                {
                    DataHandleManager.Instance().PsOrganizationHandle.CheckOrgNoIsExist(nos);
                }
            }
            ResultCode = DataHandleManager.Instance().PsUserOrgHandle.UserOrgsInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }

        /// <summary>
        /// 获取用户机构列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetUserOrgAssign([FromQuery]long UserNo)
        {
            UserOrgAssignVo assignVo = DataHandleManager.Instance().PsUserOrgHandle.GetUserOrgAssign(UserNo);
            return new JsonResponses(assignVo);
        }
        #endregion

        #region 角色权限授权
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> CreateManagementRoles([FromBody]ManagementRolesForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsRolesHandle.CheckRolesNoIsExist(model.RolesNo);
            if (model.ManagementNos != null)
            {
                foreach (var nos in model.ManagementNos)
                {
                    DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(nos);
                }
            }
            ResultCode = DataHandleManager.Instance().PsManagementRolesHandle.ManagementRolesInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementAssign([FromQuery]long RolesNo)
        {
            ManagmentAssignVo managmentAssignVo = DataHandleManager.Instance().PsManagementRolesHandle.GetManagementAssign(RolesNo);
            return new JsonResponses(managmentAssignVo);
        }
        #endregion

        #region 权限菜单授权
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> CreateManagementMenus([FromBody]ManagementMenusForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.ManagementNo);
            if (model.MenuNos != null)
            {
                foreach (var nos in model.MenuNos)
                {
                    DataHandleManager.Instance().PsMenuHandle.CheckMenuNoIsExist(nos);
                }
            }
            ResultCode = DataHandleManager.Instance().PsMenuManagementHandle.ManagementMenusInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetMenuAssign([FromQuery]long ManagementNo)
        {
            MenuAssignVo menuAssignVo = DataHandleManager.Instance().PsMenuManagementHandle.GetMenuAssign(ManagementNo);
            return new JsonResponses(menuAssignVo);
        }
        #endregion

        #region 机构角色授权
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> CreateOrgRoles([FromBody]OrgRolesForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsOrganizationHandle.CheckOrgNoIsExist(model.OrgNo);
            if (model.RolesNos != null)
            {
                foreach (var nos in model.RolesNos)
                {
                    DataHandleManager.Instance().PsRolesHandle.CheckRolesNoIsExist(nos);
                }
            }
            ResultCode = DataHandleManager.Instance().PsOrgRolesHandle.OrgRolesInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetOrgAssign([FromQuery]long OrgNo)
        {
            OrgAssignVo assignVo = DataHandleManager.Instance().PsOrgRolesHandle.GetOrgAssign(OrgNo);
            return new JsonResponses(assignVo);
        }
        #endregion
    }
}