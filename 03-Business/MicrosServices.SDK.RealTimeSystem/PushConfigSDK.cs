using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.SDK.RealTimeSystem.DataUtil;
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

namespace MicrosServices.SDK.RealTimeSystem
{
    public class PushConfigSDK
    {
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/PushConfig/GetPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/PushConfig/GetInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/PushConfig/Create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/PushConfig/Delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/PushConfig/Update";

        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<RtPushconfig> GetPageList(PageModel page, string keywords = "", long PushconfigNo = -1)
        {
            PageResponse<RtPushconfig> menus = new PageResponse<RtPushconfig>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", PushconfigNo);
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
                    menus = JsonConvert.DeserializeObject<PageResponse<RtPushconfig>>(JsonConvert.SerializeObject(data));
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
                    responses.data = JsonConvert.DeserializeObject<RtPushconfig>(JsonConvert.SerializeObject(data));
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
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses PushconfigAdd(RtPushconfig model)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("AppId", model.AppId);
                request.SetValue("Descriptions", model.Descriptions);
                request.SetValue("Enabled", model.Enabled);
                request.SetValue("ExtraProps", model.ExtraProps);
                request.SetValue("PushPort", model.PushPort);
                request.SetValue("PushType", model.PushType);
                request.SetValue("Status", model.Status);
                request.SetValue("Enabled", model.Enabled);
                request.SetValue("InputUser", model.InputUser);
                request.SetValue("InputTime", model.InputTime);
                request.SetValue("UpdateUser", model.UpdateUser);
                request.SetValue("UpdateTime", model.UpdateTime);
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
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses PushconfigUpdate(RtPushconfig model)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("AppId", model.AppId);
                request.SetValue("Descriptions", model.Descriptions);
                request.SetValue("Enabled", model.Enabled);
                request.SetValue("ExtraProps", model.ExtraProps);
                request.SetValue("PushPort", model.PushPort);
                request.SetValue("PushType", model.PushType);
                request.SetValue("Status", model.Status);
                request.SetValue("UpdateUser", model.UpdateUser);
                request.SetValue("id", model.id);
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
        public JsonResponses PushconfigDelete(int id)
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

    }
}
