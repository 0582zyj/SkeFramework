using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MicrosServices.Entities.Core.Data.Vo;
using MicrosServices.SDK.UserCenter;
using SkeFramework.Core.Network.Https.Services;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.NetworkUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using SkeFramework.Core.NetworkUtils.Bootstrap;
using MicrosServices.SDK.PermissionSystem;

namespace MicrosServices.API.PermissionSystem.Filters
{
    /// <summary>
    /// 授权过滤器
    /// </summary>
    public class AuthorizeFilterAttribute : Attribute, IAuthorizationFilter
    {

        public const string LoginSessionKey = "LoginSessionKey";
        public enum AuthorizeType : int
        {
            [Description("检查是否登录")]
            CheckLogin = 0,
            [Description("检查是否授权")]
            CheckPermission = 1,
        }
        /// <summary>
        /// 校验类型
        /// </summary>
        public int AuthorizeValue { get; set; }
        //需要的权限值
        public string BeTestedManagement { get; set; }
        /// <summary>
        /// 登录请求
        /// </summary>
        private LoginSdk loginSdk;
        /// <summary>
        /// 权限校验
        /// </summary>
        private ManagementSDK managementSDK;
        /// <summary>
        /// 检验当前登录人是否拥有权限
        /// </summary>
        /// /// <param name="authorizeType ">情况值（1为该页面需要登录可以进入，2为该页面只能有权限值的管理员才进入）</param>
        /// <param name="ManagersPermissionNum">被检验的管理员权限值</param>
        /// <returns>当拥有该权限则返回true,否则返回false</returns>
        public AuthorizeFilterAttribute(int authorizeType = 0, string ManagementValue = "")
        {
            this.AuthorizeValue = authorizeType;
            this.BeTestedManagement = ManagementValue;
            loginSdk = new LoginSdk();
            managementSDK = new ManagementSDK();
        }
        /// <summary>
        /// 授权校验
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (((AuthorizeValue >> (int)AuthorizeType.CheckLogin) & 0x01) > 0)
            {// 检查是否登录
                OperatorVo operatorVo = CheckLogin(context);
                if (operatorVo == null)
                {
                    context.Result = new JsonResult(new JsonResponses("未登录"));
                    return;
                }
            }
            if (((AuthorizeValue >> (int)AuthorizeType.CheckPermission) & 0x01) > 0)
            {// 检查权限
                JsonResponses responses = managementSDK.VaildUserManagement(this.BeTestedManagement);
                if (!responses.ValidateResponses())
                {
                    context.Result = new JsonResult(responses);
                    return;
                }
            }
        }
        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private OperatorVo CheckLogin(AuthorizationFilterContext context)
        {
            OperatorVo operatorVo = SessionUtils.Get<OperatorVo>(LoginSessionKey);
            if (operatorVo != null)
                return operatorVo;
            CookieCollection cookieCollection = new CookieCollection();
            foreach (var item in context.HttpContext.Request.Cookies)
            {
                cookieCollection.Add(new Cookie(item.Key, item.Value));
            }
            HttpWebRequestUtil.ProcessCookies(cookieCollection);
            operatorVo = loginSdk.GetCurrentOperator();
            if (operatorVo != null)
            {
                SessionUtils.Set(LoginSessionKey, operatorVo);
            }
            return operatorVo;
        }
    }
}
