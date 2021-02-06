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
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.PermissionSystem
{
    /// <summary>
    /// 菜单SDK
    /// </summary>
    public class MenuSdk
    {
        private static readonly string GetMenuListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/GetList";
        private static readonly string GetMenuPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/GetPageList";
        private static readonly string GetMenuInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/GetInfo";
        private static readonly string AddMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/Create";
        private static readonly string DeleteMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/Delete";
        private static readonly string UpdateMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/Update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/GetOptionValues";
       private static readonly string GetUserMenusListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/menu/GetUserMenusList";
       
        #region 列表
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsMenu> GetMenuList()
        {
            List<PsMenu> menus = new List<PsMenu>();
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetMenuListUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<PsMenu>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
        }
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<PsMenu> GetMenuPageList(PageModel page, string keywords ,long MenuNo)
        {
            PageResponse<PsMenu> menus = new PageResponse<PsMenu>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", MenuNo);
                request.Url = GetMenuPageUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    menus = JsonConvert.DeserializeObject<PageResponse<PsMenu>>(JsonConvert.SerializeObject(data));
                    return menus;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
        }
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsMenu> GetUserMenusList(string UserNo)
        {
            List<PsMenu> menus = new List<PsMenu>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("userNo", UserNo);
                request.Url = GetUserMenusListUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    menus = JsonConvert.DeserializeObject<List<PsMenu>>(JsonConvert.SerializeObject(data));
                    return menus;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
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
                RequestBase request = new RequestBase();
                request.SetValue("id", id.ToString());
                request.Url = GetMenuInfoUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses= JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    responses.data = JsonConvert.DeserializeObject<PsMenu>(JsonConvert.SerializeObject(data));
                }
                return responses;
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
                RequestBase request = new RequestBase();
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
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
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
        public JsonResponses MenuUpdate(PsMenu menu)
        {
            try
            {
                RequestBase request = new RequestBase();
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
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
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
                RequestBase request = new RequestBase();
                request.SetValue("id", id);
                request.Url = DeleteMenuUrl ;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
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
                RequestBase request = new RequestBase
                {
                    Url = GetOptionValueUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<OptionValue>();
        }

    }
}
