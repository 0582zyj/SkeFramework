using MicrosServices.Helper.Core.Form;
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
   public class AssignSDK
    {
        private static readonly string GetUserOrgAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/GetUserOrgAssign";
        private static readonly string CreateUserOrgsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/CreateUserOrgs";

        #region 机构角色
        /// <summary>
        /// 获取机构角色列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public JsonResponses GetUserOrgAssign(string UserNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetUserOrgAssignUrl
                };
                request.SetValue("UserNo",Convert.ToInt64( UserNo));
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 机构角色授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateUserOrgs(UserOrgsForm model)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateUserOrgsUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = JsonConvert.SerializeObject(model),
                    Method = RequestTypeEnums.POST_JSON
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion
    }
}
