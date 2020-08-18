using MicrosServices.Entities.Common;
using MicrosServices.Entities.Core.DataForm.LogQuery;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.LogSystem
{
    public class PublishLogSDK
    {
        private LogBaseSDK baseSDK = new LogBaseSDK();

        /// <summary>
        /// 获取发布日志
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<UcLoginLog> GetPublishLogPageList(PageModel page,string Title)
        {
            LogQueryForm logQuery = new LogQueryForm()
            {
                HandleUser = "Publish",
                keywords = Title,
            };
            return baseSDK.GetUcLoginLogPageList(page, logQuery);
        }
    }
}
