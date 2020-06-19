using MicrosServices.DAL.DataAccess.Repository.UserCenter.UcLoginLogHandles;
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

namespace MicrosServices.BLL.Business.UserCenter.UcLoginLogHandles
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
            keyValuePairs.Add("Password", loginForm.Password);
            keyValuePairs.Add("LoginerInfo", loginForm.LoginerInfo);
            keyValuePairs.Add("Platform", loginForm.Platform);
            keyValuePairs.Add("LoginResultType", loginResultType.ToString());
            string message = JsonConvert.SerializeObject(keyValuePairs);
            UcLoginLog loginLog = new UcLoginLog()
            {
                id= AutoIDWorker.Example.GetAutoSequence(),
                Titile = LogTypeEumns.Login.GetEnumDescription(),
                Message = message,
                LogType = LogTypeEumns.Login.ToString(),
                RequestUser = loginForm.UserName,
                RequestTime = DateTime.Now,
                InputTime = DateTime.Now,
                InputUser = loginForm.UserNo,
                Status = (int)loginResultType,
                HandleMessage = loginResultType.GetEnumDescription(),
                HandleTime = DateTime.Now,
                HandleUser = loginForm.Platform,
            };
            return this.Insert(loginLog)>0;
        }


    }
}
