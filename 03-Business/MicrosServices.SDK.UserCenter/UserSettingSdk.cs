using MicrosServices.Entities.Common;
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

namespace MicrosServices.SDK.UserCenter
{
   public class UserSettingSdk
    {
        private static string GetUserSettingInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/usersetting/get";

        private SdkUtil sdkUtil = new SdkUtil();
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        public UcUsersSetting GetUserSettingInfo(string UserNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("userNo", UserNo);
                request.Url = GetUserSettingInfoUrl;
                return sdkUtil.PostForResultVo<UcUsersSetting>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

    }
}
