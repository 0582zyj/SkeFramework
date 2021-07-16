using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Form;
using Newtonsoft.Json;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Enums;
using SkeFramework.Core.Network.Https;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.PermissionSystem
{
    /// <summary>
    /// 菜单SDK
    /// </summary>
    public class MenuSdk
    {
        private static readonly string GetMenuListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/getList";
        private static readonly string GetMenuPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/getPageList";
        private static readonly string GetMenuInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/getInfo";
        private static readonly string AddMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/create";
        private static readonly string DeleteMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/delete";
        private static readonly string UpdateMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/getOptionValues";
        private static readonly string GetUserMenusListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/getUserMenusList";

        private SdkUtil sdkUtil = new SdkUtil();
        #region 列表
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsMenu> GetMenuList()
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetMenuListUrl;
                return sdkUtil.PostForResultListVo<PsMenu>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<PsMenu>();
        }
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<PsMenu> GetMenuPageList(PageModel page, string keywords, long MenuNo)
        {
      
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", MenuNo);
                request.Url = GetMenuPageUrl;
                return sdkUtil.PostForResultVo<PageResponse<PsMenu>>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new PageResponse<PsMenu>();
        }
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsMenu> GetUserMenusList(string UserNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("userNo", UserNo);
                request.Url = GetUserMenusListUrl;
                return sdkUtil.PostForResultListVo<PsMenu>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<PsMenu>();
        }
        #endregion


        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        public JsonResponses GetPsMenuInfo(int id)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("id", id.ToString());
                request.Url = GetMenuInfoUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }


        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses MenuAdd(PsMenu menu)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("parentNo", menu.ParentNo);
                request.SetValue("name", menu.Name);
                request.SetValue("value", menu.Value);
                request.SetValue("icon", menu.icon);
                request.SetValue("url", menu.url);
                request.SetValue("sort", menu.Sort);
                request.SetValue("platformNo", menu.PlatformNo);
                request.SetValue("enabled", menu.Enabled);
                request.SetValue("inputUser", menu.InputUser);
                request.Url = AddMenuUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses MenuUpdate(PsMenu menu)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", menu.id);
                request.SetValue("menuNo", menu.MenuNo);
                request.SetValue("parentNo", menu.ParentNo);
                request.SetValue("name", menu.Name);
                request.SetValue("value", menu.Value);
                request.SetValue("icon", menu.icon);
                request.SetValue("url", menu.url);
                request.SetValue("sort", menu.Sort);
                request.SetValue("platformNo", menu.PlatformNo);
                request.SetValue("enabled", menu.Enabled);
                request.Url = UpdateMenuUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses MenuDelete(int id)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", id);
                request.Url = DeleteMenuUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues()
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetOptionValueUrl;
                return sdkUtil.PostForResultListVo<OptionValue>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<OptionValue>();
        }

    }
}
