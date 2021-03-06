﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.PermissionSystem
{
   public class NetwordConstants
    {
        public const string BASE_URL_IIS = "http://localhost/PermissionApi";
        public const string BASE_URL_PERMISSION = "https://localhost:5001";

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
            switch(config)
            {
                case "dev":
                    return NetwordConstants.BASE_URL_PERMISSION;
                case "local":
                    return NetwordConstants.BASE_URL_IIS;
                default:
                    break;
            }
            return "";
        } 
    }
}
