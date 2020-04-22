using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        /// <summary>
        /// 获取菜单树信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetMenuTreeList([FromQuery] long PlatformNo)
        {
            List<TreeNodeInfo> list = DataHandleManager.Instance().PsMenuHandle.GetPlatformMenuTree(PlatformNo);
            return new JsonResponses(list);
        }


        /// <summary>
        /// 获取权限树信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetManagementTreeList([FromQuery] long PlatformNo)
        {
            List<TreeNodeInfo> list = DataHandleManager.Instance().PsManagementHandle.GetPlatformManagementTree(PlatformNo);
            return new JsonResponses(list);
        }
    }
}