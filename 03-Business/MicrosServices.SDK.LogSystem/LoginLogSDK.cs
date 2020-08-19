using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
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
    public class LoginLogSDK
    {
        private LogBaseSDK baseSDK = new LogBaseSDK();

        /// <summary>
        /// 获取发布日志
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<UcLoginLog> GetLoginLogPageList(PageModel page, string Title)
        {
            LogQueryForm logQuery = new LogQueryForm()
            {
                HandleUser = HandleUserEumns.UserCenter.ToString(),
                keywords = Title,
            };
            return baseSDK.GetUcLoginLogPageList(page, logQuery);
        }
    }
}
