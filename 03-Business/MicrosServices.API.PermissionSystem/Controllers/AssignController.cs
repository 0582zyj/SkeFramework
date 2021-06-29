﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.API.PermissionSystem.Filters;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.Form.AssignForm;
using MicrosServices.Helper.Core.Form.Query;
using MicrosServices.Helper.Core.VO;
using MicrosServices.Helper.Core.VO.AssignVo;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssignController : ControllerBase
    {
        #region 用户角色授权
        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "user.role.assign")]
        public ActionResult<JsonResponses> CreateUserRoles([FromBody]UserRolesForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().UcUsersHandle.CheckUserNoIsExist(model.userNo);
            if (model.rolesNos != null)
            {
                foreach (var nos in model.rolesNos)
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
        /// 用户机构授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "user.org.assign")]
        public ActionResult<JsonResponses> CreateUserOrgs([FromBody]UserOrgsForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().UcUsersHandle.CheckUserNoIsExist(model.userNo);
            if (model.orgNos != null)
            {
                foreach (var nos in model.orgNos)
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

        #region 机构角色授权
        /// <summary>
        /// 机构角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "org.role.assign")]
        public ActionResult<JsonResponses> CreateOrgRoles([FromBody]OrgRolesForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsOrganizationHandle.CheckOrgNoIsExist(model.orgNo);
            if (model.rolesNos != null)
            {
                foreach (var nos in model.rolesNos)
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

        #region 角色权限授权
        /// <summary>
        /// 角色权限授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "role.management.assign")]
        public ActionResult<JsonResponses> CreateManagementRoles([FromBody]ManagementRolesForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsRolesHandle.CheckRolesNoIsExist(model.rolesNo);
            if (model.managementNos != null)
            {
                foreach (var nos in model.managementNos)
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
        public ActionResult<JsonResponses> GetManagementAssign([FromQuery]RoleManagmentQuery model)
        {
            ManagmentAssignVo managmentAssignVo = DataHandleManager.Instance().PsManagementRolesHandle.GetManagementAssign(model.RolesNo, model.ManagementType.ToList());
            return new JsonResponses(managmentAssignVo);
        }
        #endregion

        #region 权限菜单授权
        /// <summary>
        /// 权限菜单授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "management.menu.assign")]
        public ActionResult<JsonResponses> CreateManagementMenus([FromBody]ManagementMenusForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.managementNo);
            if (model.menuNos != null)
            {
                foreach (var nos in model.menuNos)
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

        #region 菜单权限授权
        /// <summary>
        /// 菜单权限授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "menu.management.assign")]
        public ActionResult<JsonResponses> CreateMenuManagements([FromBody]MenuManagementsForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsMenuHandle.CheckMenuNoIsExist(model.menuNo);
            if (model.managementNos != null)
            {
                foreach (var nos in model.managementNos)
                {
                    DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(nos);
                }
            }
            ResultCode = DataHandleManager.Instance().PsMenuManagementHandle.MenuManagementsInsert(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }

        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetMenuManagmentAssign([FromQuery]long MenuNo)
        {
            bool result = DataHandleManager.Instance().PsMenuHandle.CheckMenuNoIsExist(MenuNo);
            if (!result)
            {
                return new JsonResponses(JsonResponses.FailedCode, ErrorResultType.ERROR_MENUNO_NOT_EXISET.ToString());
            }
            MenuManagmentAssignVo assignVo = DataHandleManager.Instance().PsMenuManagementHandle.GetMenuManagmentAssignVo(MenuNo);
            return new JsonResponses(assignVo);
        }
        #endregion

        #region 分组权限授权
        /// <summary>
        /// 分组权限列表授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilterAttribute(2, "menu.management.assign")]
        public ActionResult<JsonResponses> CreateGroupManagments([FromBody]GroupManagementsForm model)
        {
            var ResultCode = -1;
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(model.managementNo);
            if (model.managementNos != null)
            {
                foreach (var nos in model.managementNos)
                {
                    DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(nos);
                }
              
            }
            ResultCode = DataHandleManager.Instance().PsMenuManagementHandle.CreateGroupManagments(model);
            return (ResultCode > 0 ? JsonResponses.Success : JsonResponses.Failed);
        }
        /// <summary>
        /// 获取分组权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetGroupManagmentsAssign([FromQuery]long managementNo)
        {
            DataHandleManager.Instance().PsManagementHandle.CheckManagementNoIsExist(managementNo);
            ManagmentGroupAssignVo assignVo = DataHandleManager.Instance().PsMenuManagementHandle.GetGroupManagmentsAssign(managementNo);
            return new JsonResponses(assignVo);
        }
        #endregion

    }
}