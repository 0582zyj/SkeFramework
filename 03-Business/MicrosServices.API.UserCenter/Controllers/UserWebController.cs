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
            if (LoginResult == LoginResultType.SUCCESS_REGISTOR)
            {
                return new JsonResponses(JsonResponses.SuccessCode, LoginResult.ToString(), registerPlatform);
            }
            return new JsonResponses(JsonResponses.FailedCode, LoginResult.ToString(), LoginResult);
        }
        /// <summary>
        /// 平台用户注销
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> CancelPlatform([FromForm]string UserNo)
        {
            if (!DataHandleManager.Instance().UcUsersHandle.CheckUserNoIsExist(UserNo))
            {
                return new JsonResponses(JsonResponses.FailedCode, LoginResultType.ERROR_USER_NOT_EXIST.ToString()
                    , LoginResultType.ERROR_USER_NOT_EXIST); 
            }
            LoginResultType LoginResult = DataHandleManager.Instance().UcUsersHandle.CancelPlatform(UserNo);
            if (LoginResult == LoginResultType.SUCCESS_CANCEL)
            {
                return new JsonResponses(JsonResponses.SuccessCode, LoginResult.ToString(), LoginResult);
            }
            return new JsonResponses(JsonResponses.FailedCode, LoginResult.ToString(), LoginResult);
            
        }
    }
}