using MicrosServices.Entities.Common;
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
   public class UserSettingSdk
    {
        private static string GetUserSettingInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/UserSetting/get";

        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        public UcUsersSetting GetUserSettingInfo(string UserNo)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("UserNo", UserNo);
                request.Url = GetUserSettingInfoUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<UcUsersSetting>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

    }
}
