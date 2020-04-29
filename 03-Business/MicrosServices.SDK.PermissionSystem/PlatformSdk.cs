using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
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
    /// 平台Sdk
    /// </summary>
    public class PlatformSdk
    {
        private static readonly string GetPlatformListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/GetList";
        private static readonly string GetPlatformPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/GetPageList";
        private static readonly string GetPlatformInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/GetInfo";
        private static readonly string AddPlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/Create";
        private static readonly string DeletePlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/Delete";
        private static readonly string UpdatePlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/Update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Platform/GetOptionValues";


        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsPlatform> GetPlatformList()
        {
            List<PsPlatform> menus = new List<PsPlatform>();
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetPlatformListUrl
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
                    return JsonConvert.DeserializeObject<List<PsPlatform>>(JsonConvert.SerializeObject(data));
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
        public PageResponse<PsPlatform> GetPlatformPageList(PageModel page, string keywords = "")
        {
            PageResponse<PsPlatform> menus = new PageResponse<PsPlatform>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.Url = GetPlatformPageUrl;
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
                    menus = JsonConvert.DeserializeObject<PageResponse<PsPlatform>>(JsonConvert.SerializeObject(data));
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
        public JsonResponses GetPsPlatformInfo(int id)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", id.ToString());
                request.Url = GetPlatformInfoUrl;
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
                    responses.data = JsonConvert.DeserializeObject<PsPlatform>(JsonConvert.SerializeObject(data));
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
        public JsonResponses PlatformAdd(PsPlatform menu)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Name", menu.Name);
                request.SetValue("Value", menu.Value);
                request.SetValue("DefaultUserName", menu.DefaultUserName);
                request.SetValue("DefaultUserNo", menu.DefaultUserNo);
                request.SetValue("InputUser", menu.InputUser);
                request.Url = AddPlatformUrl;
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
        public JsonResponses PlatformUpdate(PsPlatform platform)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", platform.id);
                request.SetValue("PlatformNo", platform.PlatformNo);
                request.SetValue("Name", platform.Name);
                request.SetValue("Value", platform.Value);
                request.SetValue("DefaultUserName", platform.DefaultUserName);
                request.SetValue("DefaultUserNo", platform.DefaultUserNo);
                request.Url = UpdatePlatformUrl;
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
        public JsonResponses PlatformDelete(int id)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", id);
                request.Url = DeletePlatformUrl;
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
        public List<OptionValue> GetOptionValues(long PlatformNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetOptionValueUrl
                };
                request.SetValue("PlatformNo", PlatformNo);
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
