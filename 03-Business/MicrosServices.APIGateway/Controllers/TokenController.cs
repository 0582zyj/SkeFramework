using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.APIGateway.Global;
using MicrosServices.APIGateway.Services;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Common.ApiGateway;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.SDK.UserCenter;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.APIGateway.Controllers
{
    /// <summary>
    /// Token控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : Controller
    {
        private UserSettingSdk userSettingSdk = new UserSettingSdk();
        private ITokenHelper tokenHelper = null;
        public TokenController(ITokenHelper _tokenHelper)
        {
            tokenHelper = _tokenHelper;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="appToken"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponses Get([FromQuery]AppToken appToken)
        {
            JsonResponses responses = JsonResponses.Failed;
            UcUsersSetting ucUsers = userSettingSdk.GetUserSettingInfo(appToken.UserNo);
            if (ucUsers == null)
            {
                responses.msg = LoginResultType.ERROR_USER_NOT_EXIST.ToString();
                return JsonResponses.Failed;
            }
            if (!( ucUsers.AppSecret.Equals(appToken.AppSecret) && ucUsers.AppId.Equals(appToken.AppId)))
            {
                responses.msg = LoginResultType.ERROR_PASSWORD_TOO_MUCH.ToString();
                return JsonResponses.Failed;
            }
            return new JsonResponses(tokenHelper.CreateAccessToken(appToken));
        }
    }
}