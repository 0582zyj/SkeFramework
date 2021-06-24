using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.PermissionSystem
{
   public class NetwordConstants
    {
        public const string BASE_URL_IIS = "http://localhost/PermissionApi";
        public const string BASE_URL_PERMISSION = "https://localhost:44346";
        public const string BASE_URL_REMOTE = "http://8.129.235.184:9023";
        public const string BASE_URL_REMOTE_LOCAL = "http://127.0.0.1:9023";

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
        private string config = "dev";
        public string GetBaseUrl()
        {
            switch(config)
            {
                case "dev":
                    return NetwordConstants.BASE_URL_PERMISSION;
                case "iis":
                    return NetwordConstants.BASE_URL_IIS;
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
