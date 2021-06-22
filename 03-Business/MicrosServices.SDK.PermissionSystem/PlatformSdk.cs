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
        private static readonly string GetPlatformListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/getList";
        private static readonly string GetPlatformPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/getPageList";
        private static readonly string GetPlatformInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/getInfo";
        private static readonly string AddPlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/create";
        private static readonly string DeletePlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/delete";
        private static readonly string UpdatePlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/platform/getOptionValues";


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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetPlatformListUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.Url = GetPlatformPageUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("id", id.ToString());
                request.Url = GetPlatformInfoUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
        /// <param name="platform"></param>
        /// <returns></returns>
        public JsonResponses PlatformAdd(PsPlatform platform)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("platformNo", platform.PlatformNo);
                request.SetValue("parentNo", platform.ParentNo);
                request.SetValue("name", platform.Name);
                request.SetValue("value", platform.Value);
                request.SetValue("defaultUserName", platform.DefaultUserName);
                request.SetValue("defaultUserNo", platform.DefaultUserNo);
                request.SetValue("inputUser", platform.InputUser);
                request.Url = AddPlatformUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", platform.id);
                request.SetValue("name", platform.Name);
                request.SetValue("value", platform.Value);
                request.SetValue("defaultUserName", platform.DefaultUserName);
                request.SetValue("parentNo", platform.ParentNo);
                request.SetValue("inputUser", platform.InputUser);
                request.Url = UpdatePlatformUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", id);
                request.Url = DeletePlatformUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetOptionValueUrl;
                request.SetValue("platformNo", PlatformNo);
                string result = HttpHelper.Example.GetWebData(request);
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
