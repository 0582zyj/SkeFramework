using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.RealTimeSystem.DataUtil
{
    public class NetwordConstants
    {
        public const string BASE_URL_IIS = "http://localhost/RealTimeApi";
        public const string BASE_URL_LOCAL = "https://localhost:44397";

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
                    return NetwordConstants.BASE_URL_LOCAL;
                case "local":
                    return NetwordConstants.BASE_URL_IIS;
                default:
                    break;
            }
            return "";
        }
    }
}
