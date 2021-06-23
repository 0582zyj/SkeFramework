using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.API.UserCenter.Filters;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.Data.Vo;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
using SkeFramework.Core.Encrypts;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.NetworkUtils;
using SkeFramework.Core.NetworkUtils.Bootstrap;

namespace MicrosServices.API.UserCenter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="loginInfoForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Login([FromForm]LoginInfoForm loginInfoForm)
        {
            loginInfoForm.MdfPas = MD5Helper.GetMD5String(loginInfoForm.Password);
            UcUsers users=new UcUsers();
            LoginResultType LoginResult = DataHandleManager.Instance().UcUsersHandle.PlatformLogin(loginInfoForm, ref users);
            if(LoginResult== LoginResultType.SUCCESS_LOGIN)
            {
                this.LoginAfterSuccess(users);
                return new JsonResponses(JsonResponses.Success.code,LoginResult.ToString(), users);
            }
            return new JsonResponses(JsonResponses.Failed.code, LoginResult.ToString(), LoginResult);
        }
        /// <summary>
        /// 登录成功后
        /// </summary>
        /// <param name="users"></param>
        private void LoginAfterSuccess(UcUsers users)
        {
            string userNo = users.UserNo;
            UcUsersSetting usersSetting= DataHandleManager.Instance().UcUsersSettingHandle.GetUcUsersSettingInfo(userNo);
            if (usersSetting == null)
                return;
            OperatorVo operatorVo = new OperatorVo()
            {
                userNo = userNo,
                platformNo = usersSetting.PlatformNo
            };
            SessionUtils.WriteSession(AuthorizeFilterAttribute.LoginSessionKey, operatorVo);
        }


        /// <summary>
        /// 获取当前登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeFilterAttribute(1)]
        public ActionResult<JsonResponses> GetCurrentOperator()
        {
            OperatorVo operatorVo = HttpContext.Session.Get<OperatorVo>(AuthorizeFilterAttribute.LoginSessionKey);
            return new JsonResponses(operatorVo);
        }
        [HttpGet]
        [AuthorizeFilterAttribute(1)]
        public async Task<IActionResult> Logout()
        {
            SessionUtils.RemoveSession(AuthorizeFilterAttribute.LoginSessionKey);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(JsonResponses.Success);
        }
    }
}