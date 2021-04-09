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


        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<RtShortMessage> GetPageList(PageModel page)
        {
            PageResponse<RtShortMessage> menus = new PageResponse<RtShortMessage>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
            
                request.Url = GetPageUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    return responses.GetDataValue<PageResponse<RtShortMessage>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
        }
    }
}
