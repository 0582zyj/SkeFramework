using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PermissionSystem.UI.WebSites.Models;

namespace PermissionSystem.UI.WebSites.Global
{
    /// <summary>
    /// 静态初始化
    /// </summary>
    public class AppBusiness
    {
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

        //public static List<ManagementDTO> SideBarList
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session == null || HttpContext.Current.Session["SideBarList"] == null)
        //        {
        //            var managementList=DataHandleManager.Instance().ManagementHandle.GetList().ToList();
        //            var managementDtoList=DataSourceAdapter.Instance().GetManagementDTOList(managementList,AppBusiness.loginModel.ManagementValue,true);
        //            HttpContext.Current.Session["SideBarList"] = managementDtoList.Where(o => o.Enabled == true).ToList();
        //        }
        //        return (List<ManagementDTO>)HttpContext.Current.Session["SideBarList"];
        //    }
        //   private set{}
        //}

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