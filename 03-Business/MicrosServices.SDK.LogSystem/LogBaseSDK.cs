﻿using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.DataForm.LogQuery;
using MicrosServices.SDK.LogSystem.DataUtil;
using Newtonsoft.Json;
using SkeFramework.Core.NetLog;
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

namespace MicrosServices.SDK.LogSystem
{
    public  class LogBaseSDK
    {
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/log/GetPageList";
       
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<UcLoginLog> GetUcLoginLogPageList(PageModel page, LogQueryForm logQueryForm)
        {
            PageResponse<UcLoginLog> menus = new PageResponse<UcLoginLog>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", logQueryForm.keywords);
                request.SetValue("queryNo", logQueryForm.queryNo);
                request.SetValue("HandleUser", logQueryForm.HandleUser);
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
                    menus = JsonConvert.DeserializeObject<PageResponse<UcLoginLog>>(JsonConvert.SerializeObject(data));
                    return menus;
                }
            }
            catch (Exception ex)
            {
               LogAgent.Info(ex.ToString());
            }
            return menus;
        }
    }
}
