using MicrosServices.DAL.DataAccess.Repository.LogSystem.UcLoginLogHandles;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
using MicrosServices.Entities.Core.DataForm.LogQuery;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
using SkeFramework.Core.Network.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.LogSystem.UcLoginLogHandles
{
    public interface IUcLoginLogHandle : IUcLoginLogHandleCommon
    {
        /// <summary>
        /// 新增登录日志
        /// </summary>
        /// <param name="UserNo"></param>
        /// <param name="UserName"></param>
        /// <param name="Md5Pas"></param>
        /// <param name="LoginerInfo"></param>
        /// <param name="platform"></param>
        /// <param name="loginResultType"></param>
        /// <returns></returns>
        bool InsertLoginLog(LoginInfoForm loginForm, LoginResultType loginResultType);
        /// <summary>
        /// 拉取代码日志
        /// </summary>
        /// <param name="RequestUser">请求人</param>
        /// <param name="message">参数</param>
        /// <param name="HandleUser">处理人</param>
        /// <param name="HandleResult">处理结果</param>
        /// <param name="HandleMessage">处理消息</param>
        /// <returns></returns>
        bool InsertPublishDeployGitLog(string RequestUser, string message, string HandleUser = "", int HandleResult = 0, string HandleMessage = "");
        /// <summary>
        /// 插入日志通用方法
        /// </summary>
        /// <param name="RequestUser"></param>
        /// <param name="message"></param>
        /// <param name="logType"></param>
        /// <param name="HandleUser"></param>
        /// <param name="HandleResult"></param>
        /// <param name="HandleMessage"></param>
        /// <returns></returns>
        bool InsertCommonLog(string RequestUser, string message, LogTypeEumns logType, string HandleUser = "", int HandleResult = 0, string HandleMessage = "");
        /// <summary>
        /// 根据查询参数获取日志列表
        /// </summary>
        /// <param name="queryForm"></param>
        /// <returns></returns>
        List<UcLoginLog> GetUcLoginLogList(PageModel page, LogQueryForm queryForm);
    }
}
