﻿using System;
using MicrosServices.Helper.Core.UserCenter.FORM;
using Newtonsoft.Json;
using SkeFramework.Core.Network.Enums;
using SkeFramework.Core.Network.Https;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.SDK.UserCenter
{
    public class LoginSdk
    {
        //private static string LoginPostUrl = NetwordConstants.BASE_URL_USERCENTER + "/api/Login/LoginPost";
        private static string LoginUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/login/login";
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public JsonResponses Login(LoginInfoForm loginInfo)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.Url = LoginUrl;
                request.SetValue("userName", loginInfo.UserName);
                request.SetValue("password", loginInfo.Password);
                request.SetValue("loginerInfo", loginInfo.LoginerInfo);
                request.SetValue("platform", loginInfo.Platform);
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
                return JsonResponses.Failed;
            }
        }
    }
}
