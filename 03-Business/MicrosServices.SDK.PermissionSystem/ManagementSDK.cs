using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
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
    public class ManagementSDK
    {
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getOptionValues";
        private static readonly string GetMenuManagementOptionsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getMenuManagementOptions";
        private static readonly string GetUserManagementListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getUserManagementList";
        private static readonly string GetManagementOptionValuesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/management/getManagementOptionValues";
        
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PsManagement> GetManagementList()
        {
            List<PsManagement> menus = new List<PsManagement>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetListUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<PsManagement>>(JsonConvert.SerializeObject(data));
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
        public PageResponse<PsManagement> GetManagementPageList(PageModel page, string keywords = "",long ManagementNo=-1)
        {
            PageResponse<PsManagement> menus = new PageResponse<PsManagement>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", ManagementNo);
                request.Url = GetPageUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    menus = JsonConvert.DeserializeObject<PageResponse<PsManagement>>(JsonConvert.SerializeObject(data));
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
        public JsonResponses GetPsManagementInfo(int id)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("id", id.ToString());
                request.Url = GetInfoUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    responses.data = JsonConvert.DeserializeObject<PsManagement>(JsonConvert.SerializeObject(data));
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
        public JsonResponses ManagementAdd(PsManagement menu)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("parentNo", menu.ParentNo);
                request.SetValue("name", menu.Name);
                request.SetValue("description", menu.Description);
                request.SetValue("value", menu.Value);
                request.SetValue("type", menu.Type);
                request.SetValue("sort", menu.Sort);
                request.SetValue("platformNo", menu.PlatformNo);
                request.SetValue("enabled", menu.Enabled);
                request.SetValue("inputUser", menu.InputUser);
                request.Url = AddUrl;
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
        public JsonResponses ManagementUpdate(PsManagement menu)
        {
            try
            {
               RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", menu.id);
                request.SetValue("managementNo", menu.ManagementNo);
                request.SetValue("parentNo", menu.ParentNo);
                request.SetValue("name", menu.Name);
                request.SetValue("value", menu.Value);
                request.SetValue("type", menu.Type);
                request.SetValue("sort", menu.Sort);
                request.SetValue("platformNo", menu.PlatformNo);
                request.SetValue("enabled", menu.Enabled);
                request.Url = UpdateUrl;
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
        public JsonResponses ManagementDelete(int id)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", id);
                request.Url = DeleteUrl;
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
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
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<ManagementOptionValue> GetManagementOptionValues(long PlatformNo,long ManagementType)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetManagementOptionValuesUrl;
                request.SetValue("platformNo", PlatformNo);
                request.SetValue("managementType", ManagementType); 
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<ManagementOptionValue>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<ManagementOptionValue>();
        }

        
        #region 菜单权限列表
        /// <summary>
        /// 获取菜单权限值
        /// </summary>
        /// <param name="menuNo"></param>
        /// <returns></returns>
        public List<ManagementOptionValue> GetMenuManagementOptions(long MenuNo)
        {
            List<ManagementOptionValue> managements = new List<ManagementOptionValue>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetMenuManagementOptionsUrl;
              
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<ManagementOptionValue>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return managements;
        }

        /// <summary>
        /// 获取用户权限值
        /// </summary>
        /// <param name="menuNo"></param>
        /// <returns></returns>
        public List<ManagementOptionValue> GetUserManagementList(string  UserNo)
        {
            List<ManagementOptionValue> managements = new List<ManagementOptionValue>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetUserManagementListUrl;

                request.SetValue("userNo", UserNo);
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<ManagementOptionValue>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return managements;
        }
        #endregion

    }
}
