using PermissionSystem.UI.WebSites.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites.Controllers.Filters
{
    public class PermissionAuthAttribute : AuthorizeAttribute
    {
        public int Situation { get; set; }
        //需要的权限值
        public long BeTestedManagement { get; set; }
        /// <summary>
        /// 检验当前登录人是否拥有权限
        /// </summary>
        /// <param name="ManagersPermissionNum">被检验的管理员权限值</param>
        /// <param name="Situation">情况值（1为该页面需要登录可以进入，2为该页面只能有权限值的管理员才进入）</param>
        /// <returns>当拥有该权限则返回true,否则返回false</returns>
        public PermissionAuthAttribute(long ManagersPermissionStr, int Situation)
        {
            this.Situation = Situation;
            this.BeTestedManagement = ManagersPermissionStr;
        }

        /// <summary>
        /// 重构跳转页面,判断权限是否能进入页面
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool flag = true;
            switch (Situation)
            {
                case 1:
                    if (AppBusiness.loginModel == null)
                    {
                        filterContext.Result = new RedirectResult("~/Login/Login");
                    }
                    break;
                case 2:
                    if (AppBusiness.loginModel == null)
                    {
                        filterContext.Result = new RedirectResult("~/Login/Login");
                    }
                    else
                    {
                        flag =true;//判断单个权限                         
                    }
                    break;
                default:
                    break;
            }
            if (!flag)//不通过返回主页
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult Json = new JsonResult();
                    Json.Data = "NoPermission";
                    Json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    filterContext.Result = Json;
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
            }
        }
    }
}