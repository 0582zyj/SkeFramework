using MicrosServices.DAL.DataAccess.Repository.LogSystem.UcLoginLogHandles;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
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
        
    }
}
