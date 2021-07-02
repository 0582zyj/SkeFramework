using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.UserCenter
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class NetwordConstants
    {
        public const string BASE_URL_USERCENTER = "http://localhost/UserCenterApi";
        public const string BASE_URL_PERMISSION = "https://localhost:5001";
        public const string BASE_URL_REMOTE = "http://8.129.235.184:9089";
        public const string BASE_URL_REMOTE_LOCAL = "http://127.0.0.1:9089";

        #region 单例模式
        /// <summary>
        /// 协议管理器
        /// </summary>
        private static NetwordConstants mSingleInstance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static NetwordConstants Instance()
        {
            if (null == mSingleInstance)
            {
                mSingleInstance = new NetwordConstants();
            }
            return mSingleInstance;
        }
        #endregion
        private string config = "remote";
        public string GetBaseUrl()
        {
            switch (config)
            {
                case "dev":
                    return NetwordConstants.BASE_URL_PERMISSION;
                case "iis":
                    return NetwordConstants.BASE_URL_USERCENTER;
                case "remote":
                    return NetwordConstants.BASE_URL_REMOTE;
                case "remote_local":
                    return NetwordConstants.BASE_URL_REMOTE_LOCAL;
                default:
                    break;
            }
            return "";
        }
    }
}
