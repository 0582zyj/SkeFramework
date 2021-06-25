using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Extends;
using MicrosServices.SDK.PermissionSystem;
using Newtonsoft.Json;
using PermissionSystem.UI.WebSites.Models;
using SkeFramework.Core.Network.Responses;

namespace PermissionSystem.UI.WebSites.Global
{
    /// <summary>
    /// 静态初始化
    /// </summary>
    public class AppBusiness
    {
        public static string PlatformCode = "SkeCloud";
        public const string SessionKey_LoginModel = "LoginModel";
        public const string SessionKey_SideBarList = "SideBarList";
        public const string SessionKey_UserManagementList = "UserManagementList";
        public const string DictionaryKey_ManagementType = "system.permission.type";
        public const string DictionaryKey_OrganizationType = "system.organization.type";
        public const string DictionaryKey_RolesType = "system.roles.type";
        
        /// <summary>
        /// 登录信息
        /// </summary>
        public static LoginModel loginModel
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionKey_LoginModel] != null)
                {
                    HttpContext.Current.Session.Timeout = 30 * 1000;
                    return (LoginModel)HttpContext.Current.Session[SessionKey_LoginModel];
                }
                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session[SessionKey_LoginModel] = value;
            }
        }
        /// <summary>
        /// 左侧菜单信息
        /// </summary>
        public static List<PsMenu> SideBarList
        {
            get
            {
                if (HttpContext.Current.Session == null || HttpContext.Current.Session[SessionKey_SideBarList] == null)
                {
                    LoginModel loginModel = AppBusiness.loginModel;
                    if (AppBusiness.loginModel != null)
                    {
                        string UserNo = AppBusiness.loginModel.UserNo;
                        HttpContext.Current.Session[SessionKey_SideBarList] = new MenuSdk().GetUserMenusList(UserNo);
                        HttpContext.Current.Session.Timeout = 30;
                    }
                }
                
                return (List<PsMenu>)HttpContext.Current.Session[SessionKey_SideBarList];
            }
            private set { }
        }

        public static void Init()
        {
            //try
            //{
            //  DataHandleFactory.SetDataHandleFactory(new IDEDataHandleFactory());
            //}
            //catch 
            //{
            //}
        }
        /// <summary>
        /// 用户权限
        /// </summary>
        public static List<ManagementOptionValue> UserManagementList
        {
            get
            {
                if (HttpContext.Current.Session == null || HttpContext.Current.Session[SessionKey_UserManagementList] == null)
                {
                    LoginModel loginModel = AppBusiness.loginModel;
                    if (AppBusiness.loginModel != null)
                    {
                        string UserNo = AppBusiness.loginModel.UserNo;
                        HttpContext.Current.Session[SessionKey_UserManagementList] = new ManagementSDK().GetUserManagementList(UserNo);
                        HttpContext.Current.Session.Timeout = 30;
                    }
                }
                return (List<ManagementOptionValue>)HttpContext.Current.Session[SessionKey_UserManagementList];
            }
        }

    }
}