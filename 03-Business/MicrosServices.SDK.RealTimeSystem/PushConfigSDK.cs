﻿using MicrosServices.Entities.Common.RealTimeSystem;
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
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/pushConfig/getPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/pushConfig/getInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/pushConfig/create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/pushConfig/delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/pushConfig/update";

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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", PushconfigNo);
                request.Url = GetPageUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("id", id.ToString());
                request.Url = GetInfoUrl;
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("appId", model.AppId);
                request.SetValue("descriptions", model.Descriptions);
                request.SetValue("enabled", model.Enabled);
                request.SetValue("extraProps", model.ExtraProps);
                request.SetValue("pushPort", model.PushPort);
                request.SetValue("pushType", model.PushType);
                request.SetValue("status", model.Status);
                request.SetValue("enabled", model.Enabled);
                request.SetValue("inputUser", model.InputUser);
                request.SetValue("inputTime", model.InputTime);
                request.SetValue("updateUser", model.UpdateUser);
                request.SetValue("updateTime", model.UpdateTime);
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
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses PushconfigUpdate(RtPushconfig model)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("appId", model.AppId);
                request.SetValue("descriptions", model.Descriptions);
                request.SetValue("enabled", model.Enabled);
                request.SetValue("extraProps", model.ExtraProps);
                request.SetValue("pushPort", model.PushPort);
                request.SetValue("pushType", model.PushType);
                request.SetValue("status", model.Status);
                request.SetValue("updateUser", model.UpdateUser);
                request.SetValue("id", model.id);
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
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses PushconfigDelete(int id)
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

    }
}
