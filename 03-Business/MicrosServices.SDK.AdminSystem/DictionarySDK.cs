using MicrosServices.Entities.Common.BaseSystem;
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

namespace MicrosServices.SDK.AdminSystem
{
   public class DictionarySDK
    {
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/GetList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/GetPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/GetInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/Create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/Delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/Update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/GetOptionValues";
        private static readonly string GetDictionaryOptionValuesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Dictionary/GetDictionaryOptionValues";

        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<BsDictionary> GetList()
        {
            List<BsDictionary> menus = new List<BsDictionary>();
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
                    return JsonConvert.DeserializeObject<List<BsDictionary>>(JsonConvert.SerializeObject(data));
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
        public PageResponse<BsDictionary> GetPageList(PageModel page, string keywords = "", long DictionaryNo = -1)
        {
            PageResponse<BsDictionary> menus = new PageResponse<BsDictionary>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", DictionaryNo);
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
                    menus = JsonConvert.DeserializeObject<PageResponse<BsDictionary>>(JsonConvert.SerializeObject(data));
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
        public JsonResponses GetInfo(int id)
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
                    responses.data = JsonConvert.DeserializeObject<BsDictionary>(JsonConvert.SerializeObject(data));
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
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses DictionaryAdd(BsDictionary menu)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("DicType", menu.DicType);
                request.SetValue("DicKey", menu.DicKey);
                request.SetValue("DicValue", menu.DicValue);
                request.SetValue("Descriptions", menu.Descriptions);
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Enabled", menu.Enabled);
                request.SetValue("InputUser", menu.InputUser);
                request.SetValue("InputTime", menu.InputTime);
                request.SetValue("UpdateUser", menu.UpdateUser);
                request.SetValue("UpdateTime", menu.UpdateTime);
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
        /// 修改
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses DictionaryUpdate(BsDictionary menu)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("DicType", menu.DicType);
                request.SetValue("DicKey", menu.DicKey);
                request.SetValue("DicValue", menu.DicValue);
                request.SetValue("Descriptions", menu.Descriptions);
                request.SetValue("PlatformNo", menu.PlatformNo);
                request.SetValue("Enabled", menu.Enabled);
                request.SetValue("InputUser", menu.InputUser);
                request.SetValue("InputTime", menu.InputTime);
                request.SetValue("UpdateUser", menu.UpdateUser);
                request.SetValue("UpdateTime", menu.UpdateTime);
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
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses DictionaryDelete(int id)
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
        public List<DictionaryOptionValue> GetDictionaryOptionValues(long PlatformNo, long DictionaryType)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetDictionaryOptionValuesUrl
                };
                request.SetValue("PlatformNo", PlatformNo);
                request.SetValue("DictionaryType", DictionaryType);
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
                    return JsonConvert.DeserializeObject<List<DictionaryOptionValue>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<DictionaryOptionValue>();
        }

    }
}
