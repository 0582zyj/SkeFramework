using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.Bootstrap.Config
{
     /// <summary>
     /// 
     /// </summary>
    public class ServerOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> Assemblies
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ScanAllAssembly
        {
            get;
            set;
        }
    }
}
