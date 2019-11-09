using System;
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
        private static string LoginUrl = NetwordConstants.BASE_URL_USERCENTER + "/api/Login/LoginPost";

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
                request.SetValue("UserName", loginInfo.UserName);
                request.SetValue("Password", loginInfo.Password);
                request.SetValue("LoginerInfo", loginInfo.LoginerInfo);
                request.SetValue("Platform", loginInfo.Platform);

                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = LoginUrl,
                    PostData = JsonConvert.SerializeObject(request.ParameterValue),
                    Method = RequestTypeEnums.POST
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                return JsonResponses.Failed;
            }
        }
    }
}
