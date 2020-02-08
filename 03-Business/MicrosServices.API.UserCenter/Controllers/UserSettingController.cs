using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.UserCenter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSettingController : ControllerBase
    {
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetUserSettingInfo([FromQuery]string UserNo)
        {
            UcUsersSetting Info = new UcUsersSetting();
            Info = DataHandleManager.Instance().UcUsersSettingHandle.GetUcUsersSettingInfo(UserNo);
            return new JsonResponses(Info);
        }

    }
}