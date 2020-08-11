using MicrosServices.DAL.DataAccess.Repository.LogSystem.UcLoginLogHandles;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Constants;
using Newtonsoft.Json;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Common.Enums;
using SkeFramework.Core.SnowFlake;
using MicrosServices.Helper.Core.UserCenter.FORM;

namespace MicrosServices.BLL.Business.LogSystem.UcLoginLogHandles
{
    public class UcLoginLogHandle : UcLoginLogHandleCommon, IUcLoginLogHandle
    {
        public UcLoginLogHandle(IRepository<UcLoginLog> dataSerialer)
            : base(dataSerialer)
        {
        }
        /// <summary>
        /// 登陆业务
        /// </summary>
        /// <param name="UserNo">用户编号</param>
        /// <param name="UserName">用户名</param>
        /// <param name="Md5Pas">密码</param>
        /// <param name="LoginerInfo">登陆者信息</param>
        /// <param name="platform">平台code</param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InsertLoginLog(LoginInfoForm loginForm, LoginResultType loginResultType)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("UserName", loginForm.UserName);
            keyValuePairs.Add("MdfPas", loginForm.MdfPas);
            keyValuePairs.Add("LoginerInfo", loginForm.LoginerInfo);
            keyValuePairs.Add("Platform", loginForm.Platform);
            string message = JsonConvert.SerializeObject(keyValuePairs);
            UcLoginLog loginLog = new UcLoginLog()
            {
                id = AutoIDWorker.Example.GetAutoSequence(),
                Titile = LogTypeEumns.Login.GetEnumDescription(),
                Message = message,
                LogType = LogTypeEumns.Login.ToString(),
                RequestUser = loginForm.UserNo,
                RequestTime = DateTime.Now,
                InputTime = DateTime.Now,
                InputUser = loginForm.UserNo,
                Status = 0,
                HandleResult = (int)loginResultType,
                HandleMessage = loginResultType.GetEnumDescription(),
                HandleTime = DateTime.Now,
                HandleUser = loginForm.Platform,
                ExpiresIn = 60 * 60 * 1000,
            };
            return this.Insert(loginLog)>0;
        }

        /// <summary>
        /// 拉取代码日志
        /// </summary>
        /// <param name="RequestUser">请求人</param>
        /// <param name="message">参数</param>
        /// <param name="HandleUser">处理人</param>
        /// <param name="HandleResult">处理结果</param>
        /// <param name="HandleMessage">处理消息</param>
        /// <returns></returns>
        public bool InsertPublishDeployGitLog(string RequestUser, string message,string HandleUser="", int HandleResult = 0, string HandleMessage = "")
        {
           return this.InsertCommonLog( RequestUser,  message, LogTypeEumns.PublishGit,  HandleUser,  HandleResult,  HandleMessage);
        }

        /// <summary>
        /// 插入基础日志
        /// </summary>
        /// <param name="RequestUser">请求人</param>
        /// <param name="message">参数</param>
        /// <param name="HandleUser">处理人</param>
        /// <param name="HandleResult">处理结果</param>
        /// <param name="HandleMessage">处理消息</param>
        /// <returns></returns>
        public bool InsertCommonLog(string RequestUser, string message, LogTypeEumns logType, string HandleUser = "", int HandleResult = 0, string HandleMessage = "")
        {
            UcLoginLog loginLog = new UcLoginLog()
            {
                id = AutoIDWorker.Example.GetAutoSequence(),
                Titile = logType.GetEnumDescription(),
                Message = message,
                LogType = logType.ToString(),
                RequestUser = RequestUser,
                RequestTime = DateTime.Now,
                InputTime = DateTime.Now,
                InputUser = RequestUser,
                Status = 0,
                HandleResult = HandleResult,
                HandleMessage = HandleMessage.Length>1500? HandleMessage.Substring(0,1499):HandleMessage,
                HandleTime = DateTime.Now,
                HandleUser = String.IsNullOrEmpty(HandleUser) ? RequestUser : HandleUser,
                ExpiresIn = 60 * 60 * 1000,
            };
            return this.Insert(loginLog) > 0;
        }

    }
}
