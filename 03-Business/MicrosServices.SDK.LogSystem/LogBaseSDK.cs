using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.DataForm.LogQuery;
using MicrosServices.SDK.LogSystem.DataUtil;
using Newtonsoft.Json;
using SkeFramework.Core.NetLog;
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

namespace MicrosServices.SDK.LogSystem
{
    public  class LogBaseSDK
    {
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/log/etPageList";

        private SdkUtil sdkUtil = new SdkUtil();
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<UcLoginLog> GetUcLoginLogPageList(PageModel page, LogQueryForm logQueryForm)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageIndex", page.PageIndex,true);
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", logQueryForm.keywords);
                request.SetValue("queryNo", logQueryForm.queryNo);
                request.SetValue("handleUser", logQueryForm.HandleUser);
                request.Url = GetPageUrl;
                return sdkUtil.PostForResultVo<PageResponse<UcLoginLog>>(request);
            }
            catch (Exception ex)
            {
               LogAgent.Info(ex.ToString());
            }
            return new PageResponse<UcLoginLog>();
        }
    }
}
