using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
using SkeFramework.Core.ApiCommons.Responses;

namespace MicrosServices.API.UserCenter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET api/values
        [HttpPost]
        public ActionResult<JsonResponses> Login([FromForm]LoginInfoForm loginInfoForm)
        {
            string MdfPas = "";
            UcUsers users=new UcUsers();
            LoginResultType LoginResult = DataHandleManager.Instance().UcUsersHandle.Login(loginInfoForm.UserName, MdfPas, loginInfoForm.LoginerInfo,
                loginInfoForm.Platform, ref users);
            if(LoginResult== LoginResultType.SUCCESS_LOGIN)
            {
                return new JsonResponses(JsonResponses.Success.code,LoginResult.ToString(), LoginResult);
            }
            return new JsonResponses(JsonResponses.Failed.code, LoginResult.ToString(), LoginResult);

        }

    }
}