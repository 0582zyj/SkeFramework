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
    public class RolesSDK
    {
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/getList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/getPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/getInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/roles/getOptionValues";

        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsRoles> GetRolesList()
        {
            List<PsRoles> menus = new List<PsRoles>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetListUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<PsRoles>>(JsonConvert.SerializeObject(data));
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
        public PageResponse<PsRoles> GetRolesPageList(PageModel page, string keywords = "")
        {
            PageResponse<PsRoles> menus = new PageResponse<PsRoles>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetPageUrl;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    menus = JsonConvert.DeserializeObject<PageResponse<PsRoles>>(JsonConvert.SerializeObject(data));
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
        public JsonResponses GetPsRolesInfo(int id)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetInfoUrl;
                request.SetValue("id", id.ToString());
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    responses.data = JsonConvert.DeserializeObject<PsRoles>(JsonConvert.SerializeObject(data));
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
        public JsonResponses RolesAdd(PsRoles menu)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = AddUrl;
                request.SetValue("parentNo", menu.ParentNo);
                request.SetValue("name", menu.Name);
                request.SetValue("description", menu.Description);
                request.SetValue("platformNo", menu.PlatformNo);
                request.SetValue("enabled", menu.Enabled);
                request.SetValue("inputUser", menu.InputUser);
                request.SetValue("managementValue", menu.ManagementValue);
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
        public JsonResponses RolesUpdate(PsRoles menu)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = UpdateUrl;
                request.SetValue("id", menu.id);
                request.SetValue("rolesNo", menu.RolesNo);
                request.SetValue("parentNo", menu.ParentNo);
                request.SetValue("description", menu.Description);
                request.SetValue("name", menu.Name);
                request.SetValue("managementValue", menu.ManagementValue);
                request.SetValue("platformNo", menu.PlatformNo);
                request.SetValue("enabled", menu.Enabled);
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
        public JsonResponses RolesDelete(int id)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = DeleteUrl;
                request.SetValue("id", id);
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
        public List<OptionValue> GetOptionValues()
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = GetOptionValueUrl;
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
