using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder
{
    /// <summary>
    /// 软件系统信息
    /// </summary>
    public class SoftwareVersion
    {
        /// <summary>
        /// 系统版本号
        /// </summary>
        public static string Version = SoftwareVersion.GetVersion();

        public static string GetVersion()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return string.Format("v{0}.{1}.{2}", version.Major.ToString(), version.Minor.ToString(),version.MinorRevision.ToString());
        }

       
    }
}
