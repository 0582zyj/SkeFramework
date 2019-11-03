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
        private static string LoginUrl = "https://localhost:5001/api/Login/LoginGet";
        public JsonResponses Login(LoginInfoForm loginInfo)
        {
            RequestBase request = new RequestBase();
            request.Url = LoginUrl;
            request.SetValue("UserName", loginInfo.UserName);
            request.SetValue("Password", loginInfo.Password);
            request.SetValue("LoginerInfo", loginInfo.LoginerInfo);
            request.SetValue("Platform", loginInfo.Platform);
            string result = HttpHelper.Example.GetWebData(new BrowserPara()
            {
                Uri = request.GetReqUrl(),
                PostData = "",
                Method = RequestTypeEnums.GET
            });
            return JsonConvert.DeserializeObject<JsonResponses>(result);
        }
    }
}
