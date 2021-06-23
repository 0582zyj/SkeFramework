using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.API.UserCenter.Filters;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.Data.Vo;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.NetworkUtils.Bootstrap;

namespace MicrosServices.API.UserCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingController : ControllerBase
    {
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public ActionResult<JsonResponses> GetUserSettingInfo([FromQuery]string UserNo)
        {            
            UcUsersSetting Info = new UcUsersSetting();
            Info = DataHandleManager.Instance().UcUsersSettingHandle.GetUcUsersSettingInfo(UserNo);
            return new JsonResponses(Info);
        }
    }
}