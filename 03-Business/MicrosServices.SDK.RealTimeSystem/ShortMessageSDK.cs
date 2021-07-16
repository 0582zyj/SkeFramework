using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.SDK.RealTimeSystem.DataUtil;
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

namespace MicrosServices.SDK.RealTimeSystem
{
    public class ShortMessageSDK
    {

        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/shortmessage/getPageList";

        private SdkUtil sdkUtil = new SdkUtil();

        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<RtShortMessage> GetPageList(PageModel page)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
            
                request.Url = GetPageUrl;
                return sdkUtil.PostForResultVo<PageResponse<RtShortMessage>>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new PageResponse<RtShortMessage>();
        }
    }
}
