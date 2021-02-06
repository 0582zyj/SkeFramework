using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Form;
using Newtonsoft.Json;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Enums;
using SkeFramework.Core.Network.Https;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.SDK.PermissionSystem
{
    /// <summary>
    /// 机构SDK
    /// </summary>
    public class OrganizationSdk
    {
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/getList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/getPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/getInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/organization/getOptionValues";
     
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<PsOrganization> GetOrganizationPageList(PageModel page, string keywords = "",long OrgNo=-1)
        {
            PageResponse<PsOrganization> menus = new PageResponse<PsOrganization>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", OrgNo);
                request.Url = GetPageUrl;
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
                    menus = JsonConvert.DeserializeObject<PageResponse<PsOrganization>>(JsonConvert.SerializeObject(data));
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
        public JsonResponses GetPsOrganizationInfo(int id)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", id.ToString());
                request.Url = GetInfoUrl;
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
                    responses.data = JsonConvert.DeserializeObject<PsOrganization>(JsonConvert.SerializeObject(data));
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
        /// <returns></returns>
        public JsonResponses OrganizationAdd(PsOrganization model)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", model.id);
                request.SetValue("orgNo", model.OrgNo);
                request.SetValue("parentNo", model.ParentNo);
                request.SetValue("name", model.Name);
                request.SetValue("description", model.Description);
                request.SetValue("category", model.Category);
                request.SetValue("platformNo", model.PlatformNo);
                request.SetValue("enabled", model.Enabled);
                request.SetValue("inputUser", model.InputUser);
                request.Url = AddUrl;
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
        /// <returns></returns>
        public JsonResponses OrganizationUpdate(PsOrganization model)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", model.id);
                request.SetValue("orgNo", model.OrgNo);
                request.SetValue("parentNo", model.ParentNo);
                request.SetValue("name", model.Name);
                request.SetValue("description", model.Description);
                request.SetValue("category", model.Category);
                request.SetValue("platformNo", model.PlatformNo);
                request.SetValue("enabled", model.Enabled);
                request.Url = UpdateUrl;
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
        /// 删除机构
        /// </summary>
        /// <returns></returns>
        public JsonResponses OrganizationDelete(int id)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", id);
                request.Url = DeleteUrl;
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
