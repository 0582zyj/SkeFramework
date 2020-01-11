using MicrosServices.Helper.Core.UserCenter.FORM;
using Newtonsoft.Json;
using SkeFramework.Core.Network.Enums;
using SkeFramework.Core.Network.Https;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.UserCenter
{
    public class UserSDK
    {
        private static string RegisterPlatfromUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/UserWeb/RegisterPlatfrom";
        private static string CancelPlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/UserWeb/CancelPlatform";
        
        /// <summary>
        /// 平台账号注册
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public JsonResponses RegisterPlatfrom(RegisterPlatformForm registerPlatform)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.Url = RegisterPlatfromUrl;
                request.SetValue("UserName", registerPlatform.UserName);
                request.SetValue("Password", registerPlatform.Password);
                request.SetValue("UserNo", registerPlatform.UserNo);
                request.SetValue("Phone", registerPlatform.Phone);
                request.SetValue("Email", registerPlatform.Email);
                request.SetValue("InputUser", registerPlatform.InputUser);
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
                });
               return  JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonResponses.Failed;
            }
        }

        /// <summary>
        /// 平台账号注册
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public JsonResponses CancelPlatform(string UserNo)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.Url = CancelPlatformUrl;
                request.SetValue("UserNo", UserNo);
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
