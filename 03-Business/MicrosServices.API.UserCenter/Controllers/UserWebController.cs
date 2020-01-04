using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
using SkeFramework.Core.Encrypts;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.UserCenter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserWebController : ControllerBase
    {
        /// <summary>
        /// 平台用户注册接口
        /// </summary>
        /// <param name="registerPlatform"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> RegisterPlatfrom([FromForm]RegisterPlatformForm registerPlatform)
        {
            string MdfPas = MD5Helper.GetMD5String(registerPlatform.Password);
            registerPlatform.Password = MdfPas;
            if(String.IsNullOrEmpty(registerPlatform.Phone))
            {
                registerPlatform.Phone = ConstData.DefaultNo.ToString();
            }
            if (String.IsNullOrEmpty(registerPlatform.Email))
            {
                registerPlatform.Email = ConstData.DefaultNo.ToString();
            }
            LoginResultType LoginResult = DataHandleManager.Instance().UcUsersHandle.RegisterPlatform(registerPlatform);
            if (LoginResult == LoginResultType.SUCCESS_LOGIN)
            {
                return new JsonResponses(JsonResponses.Success.code, LoginResult.ToString(), registerPlatform);
            }
            return new JsonResponses(JsonResponses.Failed.code, LoginResult.ToString(), LoginResult);
        }

    }
}