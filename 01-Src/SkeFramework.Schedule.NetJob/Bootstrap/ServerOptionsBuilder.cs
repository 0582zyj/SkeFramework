using Microsoft.Extensions.DependencyInjection;
using SkeFramework.Schedule.NetJob.Bootstrap.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.Bootstrap
{
    public class ServerOptionsBuilder
    {
        readonly List<Assembly> assemblies = new List<Assembly>();

        /// <summary>
        /// Set specific assembly to scan crontab object
        /// </summary>
        /// <param name="assembly"></param>
        public void SetAssembly(Assembly assembly)
        {
            if (!assemblies.Contains(assembly))
            {
                assemblies.Add(assembly);
            }
        }

        /// <summary>
        /// Set specific assembly to scan crontab object
        /// </summary>
        /// <param name="assemblyString"></param>
        public void SetAssembly(string assemblyString)
        {
            var assembly = Assembly.Load(assemblyString);
            if (!assemblies.Contains(assembly))
            {
                assemblies.Add(assembly);
            }
        }

        /// <summary>
        /// Scan all assembly
        /// </summary>
        public bool ScanAllAssembly
        {
            get;
            set;
        }

        internal void BuildServices(IServiceCollection services)
        {
            var options = new ServerOptions()
            {
                Assemblies = assemblies,
                ScanAllAssembly = ScanAllAssembly
            };
            services.AddSingleton(options);
            services.AddSingleton<ServerBootstrap>();
        }

    }
}
