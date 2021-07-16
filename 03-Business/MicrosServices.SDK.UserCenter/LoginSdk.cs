using System;
using System.Net;
using MicrosServices.Entities.Core.Data.Vo;
using MicrosServices.Helper.Core.UserCenter.FORM;
using Newtonsoft.Json;
using SkeFramework.Core.Network.DataUtility;
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
        private static string GetCurrentOperatorUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/login/getCurrentOperator";

        private SdkUtil sdkUtil = new SdkUtil();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public JsonResponses Login(LoginInfoForm loginInfo)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = LoginUrl;
                request.SetValue("userName", loginInfo.UserName);
                request.SetValue("password", loginInfo.Password);
                request.SetValue("loginerInfo", loginInfo.LoginerInfo);
                request.SetValue("platform", loginInfo.Platform);
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonResponses.Failed;
            }
        }


        /// <summary>
        /// 获取当前登录信息
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public OperatorVo GetCurrentOperator()
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetCurrentOperatorUrl;
                return sdkUtil.PostForResultVo<OperatorVo>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        

    }
}
