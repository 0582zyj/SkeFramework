using MicrosServices.Entities.Common;
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
        private static string GetMenuListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Menu/GetList";
        private static string GetMenuPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Menu/GetPageList";
        private static string GetMenuInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Menu/GetMenuInfo";
        private static string AddMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Menu/Add";
        private static string DeleteMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Menu/Delete";
        private static string UpdateMenuUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Menu/Update";
        

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
                RequestBase request = new RequestBase();
                request.Url = GetMenuListUrl;
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
        public PageResponse<PsMenu> GetMenuPageList(PageModel page, string keywords = "")
        {
            PageResponse<PsMenu> menus = new PageResponse<PsMenu>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", keywords);
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
                request.SetValue("ParentNo", menu.ParentNo);
                request.SetValue("Name", menu.Name);
                request.SetValue("Value", menu.Value);
                request.SetValue("icon", menu.icon);
                request.SetValue("url", menu.url);
                request.SetValue("Sort", menu.Sort);
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Enabled", menu.Enabled);
                request.SetValue("InputUser", menu.InputUser);
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
                request.SetValue("ParentNo", menu.ParentNo);
                request.SetValue("Name", menu.Name);
                request.SetValue("Value", menu.Value);
                request.SetValue("icon", menu.icon);
                request.SetValue("url", menu.url);
                request.SetValue("Sort", menu.Sort);
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Enabled", menu.Enabled);
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
    }
}
