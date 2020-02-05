using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.VO;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssignController : ControllerBase
    {


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
    }
}