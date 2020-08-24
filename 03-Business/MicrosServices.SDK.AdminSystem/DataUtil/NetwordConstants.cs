using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.AdminSystem
{
    public class NetwordConstants
    {
        public const string BASE_URL_PUBLISHDEPLOY_LOCAL = "http://localhost/AdminServerApi";
        public const string BASE_URL_PUBLISHDEPLOY = "https://localhost:44397";

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
        private string config = "local";
        public string GetBaseUrl()
        {
            switch (config)
            {
                case "dev":
                    return NetwordConstants.BASE_URL_PUBLISHDEPLOY;
                case "local":
                    return NetwordConstants.BASE_URL_PUBLISHDEPLOY_LOCAL;
                default:
                    break;
            }
            return "";
        }
    }
}
