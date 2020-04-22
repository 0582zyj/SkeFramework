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
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/Create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/Delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/Update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetOptionValues";
        private static readonly string GetMenuManagementOptionsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetMenuManagementOptions";
        private static readonly string GetUserManagementListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetUserManagementList";
        private static readonly string GetManagementOptionValuesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Management/GetManagementOptionValues";
        
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
                RequestBase request = new RequestBase
                {
                    Url = GetListUrl
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
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", ManagementNo);
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
                RequestBase request = new RequestBase();
                request.SetValue("ParentNo", menu.ParentNo);
                request.SetValue("Name", menu.Name);
                request.SetValue("Description", menu.Description);
                request.SetValue("Value", menu.Value);
                request.SetValue("Type", menu.Type);
                request.SetValue("Sort", menu.Sort);
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Enabled", menu.Enabled);
                request.SetValue("InputUser", menu.InputUser);
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
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses ManagementUpdate(PsManagement menu)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", menu.id);
                request.SetValue("ManagementNo", menu.ManagementNo);
                request.SetValue("ParentNo", menu.ParentNo);
                request.SetValue("Name", menu.Name);
                request.SetValue("Value", menu.Value);
                request.SetValue("Type", menu.Type);
                request.SetValue("Sort", menu.Sort);
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Enabled", menu.Enabled);
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
        /// 删除菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses ManagementDelete(int id)
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
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<ManagementOptionValue> GetManagementOptionValues(long PlatformNo,long ManagementType)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetManagementOptionValuesUrl
                };
                request.SetValue("PlatformNo", PlatformNo);
                request.SetValue("ManagementType", ManagementType); 
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
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        public List<ManagementOptionValue> GetMenuManagementOptions(long MenuNo)
        {
            List<ManagementOptionValue> managements = new List<ManagementOptionValue>();
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetMenuManagementOptionsUrl
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
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        public List<ManagementOptionValue> GetUserManagementList(string  UserNo)
        {
            List<ManagementOptionValue> managements = new List<ManagementOptionValue>();
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetUserManagementListUrl
                };
                request.SetValue("UserNo", UserNo);
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
