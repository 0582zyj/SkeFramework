using Microsoft.Extensions.DependencyModel;
using SkeFramework.Core.NetLog;
using SkeFramework.Schedule.NetJob.Bootstrap.Config;
using SkeFramework.Schedule.NetJob.DataAttributes;
using SkeFramework.Schedule.NetJob.DataHandle;
using SkeFramework.Schedule.NetJob.DataHandle.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.Bootstrap
{
   public class ServerBootstrap : IDisposable
    {
        readonly Dictionary<string, JobExecutor> executorDict = new Dictionary<string, JobExecutor>();
        readonly CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public ServerBootstrap(IServiceProvider services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var options = services.GetService(Type.GetType("ServerOptions"));
            if (options == null)
            {
                options = new ServerOptions();
            }
            var types = GetTypeInfos(options as ServerOptions);
            var list = new List<JobExecutor>();
            foreach (var type in types)
            {
                foreach (var method in type.DeclaredMethods)
                {
                    var attribute = method.GetCustomAttribute<ScheduleAttribute>();
                    if (attribute != null)
                    {
                        if (string.IsNullOrEmpty(attribute.Name))
                        {
                            throw new CustomAttributeFormatException("Crontab name is empty");
                        }
                        var arr = attribute.Schedule.Split('|');
                        if (arr.Length == 0)
                        {
                            throw new CustomAttributeFormatException($"Crontab '{attribute.Name}' does not have any schedule");
                        }
                        var schedules = new JobSchedule[arr.Length];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (JobSchedule.TryParse(arr[i], out JobSchedule schedule))
                            {
                                schedules[i] = schedule;
                            }
                            else
                            {
                                throw new CustomAttributeFormatException($"Crontab '{attribute.Name}' schedule '{arr[i]}' format error");
                            }
                        }
                        var name = attribute.Name;
                        if (string.IsNullOrEmpty(name))
                        {
                            name = method.Name;
                        }
                        var item = new JobExecutor(name, services, cts.Token, type, method, schedules, attribute.AllowConcurrentExecution, attribute.RunImmediately);
                        if (!executorDict.ContainsKey(item.Name))
                        {
                            executorDict.Add(item.Name, item);
                            if (attribute.AutoEnable)
                            {
                                item.Enable();
                            }
                        }
                        else
                        {
                            throw new CustomAttributeFormatException($"Crontab '{item.Name}' name is duplicate");
                        }
                    }
                }
            }

            var dt = DateTime.Now;
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            var next = dt.AddMinutes(1);
            var due = Convert.ToInt32(Math.Ceiling((next - DateTime.Now).TotalMilliseconds));

            Task.Factory.StartNew(async () => {
                try
                {
                    await Task.Delay(due, cts.Token);
                    Execute();
                }
                catch (Exception ex)
                {
                    LogAgent.Error(ex.ToString());
                }
            }, cts.Token);
        }

        private void Execute()
        {
            if (cts.Token.IsCancellationRequested)
            {
                return;
            }
            var list = executorDict.Values.ToList();
            foreach (var item in list)
            {
                Task.Factory.StartNew(() => {
                    item.Run();
                });
            }
            var dt = DateTime.Now;
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            var next = dt.AddMinutes(1);
            var due = Convert.ToInt32(Math.Ceiling((next - DateTime.Now).TotalMilliseconds));
            Task.Factory.StartNew(async () => {
                try
                {
                    await Task.Delay(due, cts.Token);
                    Execute();
                }
                catch (Exception ex)
                {
                    LogAgent.Error(ex.ToString());
                }
            }, cts.Token);
        }

        /// <summary>
        /// Get excutor list
        /// </summary>
        /// <returns></returns>
        public List<JobExecutor> GetExecutors()
        {
            return executorDict.Values.ToList();
        }

        /// <summary>
        /// Get specified executor
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JobExecutor GetExecutor(string name)
        {
            if (executorDict.TryGetValue(name, out JobExecutor executor))
            {
                return executor;
            }
            else
            {
                throw new ArgumentException($"Crontab name '{name}' dose not exists");
            }
        }

        private List<TypeInfo> GetTypeInfos(ServerOptions options)
        {
            HashSet<Assembly> assemblys = new HashSet<Assembly>();
            if (options.ScanAllAssembly)
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                var dependencyContext = DependencyContext.Load(entryAssembly);
                if (dependencyContext == null)
                {
                    assemblys.Add(entryAssembly);
                }
                else
                {
                    var assName = Assembly.GetExecutingAssembly().GetName().Name;
                    var libs = dependencyContext.RuntimeLibraries.Where(lib => lib.Dependencies.Any(dep => string.Equals(assName, dep.Name, StringComparison.Ordinal)));
                    var assNames = libs.SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext));
                    foreach (var name in assNames)
                    {
                        var assembly = Assembly.Load(new AssemblyName(name.Name));
                        assemblys.Add(assembly);
                    }
                }
            }
            if (options.Assemblies != null && options.Assemblies.Count > 0)
            {
                foreach (var assembly in options.Assemblies)
                {
                    assemblys.Add(assembly);
                }
            }
            if (assemblys.Count == 0)
            {
                assemblys.Add(Assembly.GetEntryAssembly());
            }

            var types = assemblys.SelectMany(a => a.DefinedTypes.Where(y => y.GetCustomAttribute<JobAttribute>() != null)).ToList();
            return types;
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    cts.Cancel();
                    cts.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CrontabService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// Dispose service and stop all executor
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion



    }
}
