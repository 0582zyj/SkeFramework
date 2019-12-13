﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicrosServices.Entities.Common;
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
        public static MenuSdk menuSdk = new MenuSdk();
        public static string PlatformCode = "SkeCloud";
        /// <summary>
        /// 登录信息
        /// </summary>
        public static LoginModel loginModel
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["LoginModel"] != null)
                {
                    return (LoginModel)HttpContext.Current.Session["LoginModel"];
                }
                return new LoginModel();
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginModel"] = value;
            }
        }

        public static List<PsMenu> SideBarList
        {
            get
            {
                if (HttpContext.Current.Session == null || HttpContext.Current.Session["SideBarList"] == null)
                {
                    HttpContext.Current.Session["SideBarList"] = menuSdk.GetMenuList(); 
                }
                return (List<PsMenu>)HttpContext.Current.Session["SideBarList"];
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
    

    }
}