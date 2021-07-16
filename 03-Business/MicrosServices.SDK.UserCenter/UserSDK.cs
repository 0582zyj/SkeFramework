using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.UserCenter.FORM;
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
    public class UserSDK
    {
        private static string RegisterPlatfromUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/user/registerPlatform";
        private static string CancelPlatformUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/user/cancelPlatform";
        private static string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/user/pageList";
        private static string GetUserInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/user/get";

        private SdkUtil sdkUtil = new SdkUtil();
        #region 平台管理
        /// <summary>
        /// 平台账号注册
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public JsonResponses RegisterPlatfrom(RegisterPlatformForm registerPlatform)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = RegisterPlatfromUrl;
                request.SetValue("username", registerPlatform.UserName);
                request.SetValue("password", registerPlatform.Password);
                request.SetValue("userNo", registerPlatform.UserNo);
                request.SetValue("phone", registerPlatform.Phone);
                request.SetValue("email", registerPlatform.Email);
                request.SetValue("inputUser", registerPlatform.InputUser);
                request.SetValue("platformNo", registerPlatform.PlatformNo);
                return sdkUtil.PostForVo(request);
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
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.Url = CancelPlatformUrl;
                request.SetValue("userNo", UserNo);
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonResponses.Failed;
            }
        }
        #endregion

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public PageResponse<UcUsers> GetUserPageList(PageModel page, string keywords)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.Url = GetPageUrl;
                return sdkUtil.PostForResultVo<PageResponse<UcUsers>>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new PageResponse<UcUsers>();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public UcUsers GetUserInfo(long UserNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("userNo", UserNo.ToString());
                request.Url = GetUserInfoUrl;
                return sdkUtil.PostForResultVo<UcUsers>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

    }
}
