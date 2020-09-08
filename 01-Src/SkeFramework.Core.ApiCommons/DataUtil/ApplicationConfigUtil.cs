using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.Core.ApiCommons.DataUtil
{
    /// <summary>
    /// 配置工具类
    /// </summary>
    public class ApplicationConfigUtil
    {
        static IConfiguration Configuration { get; set; }
        static string contentPath { get; set; }


        public ApplicationConfigUtil(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string GetAppSeting(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception ex)
            {
                NetLog.LogAgent.Error(ex.ToString());
            }
            return "";
        }
    }
}
