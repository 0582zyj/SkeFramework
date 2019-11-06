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
        private static string LoginUrl = "https://localhost:5001/api/Login/Login";
        private static string LoginUrl1 = "https://localhost:5001/api/Login/LoginGet";
        private static string LoginUrl2 = "https://localhost:5001/api/Login/LoginPost";
        private static string TestUrl = "https://localhost:5001/api/values";
        public JsonResponses Login(LoginInfoForm loginInfo)
        {
            RequestBase request = new RequestBase();
            request.Url = LoginUrl;
            request.SetValue("UserName", loginInfo.UserName);
            request.SetValue("Password", loginInfo.Password);
            request.SetValue("LoginerInfo", loginInfo.LoginerInfo);
            request.SetValue("Platform", loginInfo.Platform);
            string result;
            //result = HttpHelper.Example.GetWebData(new BrowserPara()
            //{
            //    Uri = request.Url,
            //    PostData = JsonConvert.SerializeObject(request.ParameterValue),
            //    Method = RequestTypeEnums.POST
            //});

             result = HttpHelper.Example.GetWebData(new BrowserPara()
            {
                Uri = LoginUrl2,
                PostData = JsonConvert.SerializeObject(request.ParameterValue),
                 Method = RequestTypeEnums.POST
            });
             return JsonConvert.DeserializeObject<JsonResponses>(result);
        }
    }
}
