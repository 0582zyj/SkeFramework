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

namespace MicrosServices.SDK.PermissionSystem
{
   public class TreeSDK
    {
        private  readonly string GetMenuTreeListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Tree/GetMenuTreeList";
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetMenuTreeList(long PlatformNo)
        {
            List<TreeNodeInfo> menus = new List<TreeNodeInfo>();
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetMenuTreeListUrl
                };
                request.SetValue("PlatformNo", PlatformNo);
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<TreeNodeInfo>>(JsonConvert.SerializeObject(data));
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
