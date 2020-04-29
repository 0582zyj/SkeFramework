using SkeFramework.Schedule.NetJob.DataAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MicrosServices.API.PermissionSystem.Handles.ScheduleJob
{
    [JobAttribute]
    public class DemoJob
    {
        [ScheduleAttribute("test1", "*/1", RunImmediately = true)]
        public void Test1(CancellationToken token)
        {
           Console.WriteLine (DateTime.Now.ToString() + " test1 start");
            Thread.Sleep(5000);
            Console.WriteLine(DateTime.Now.ToString() + " test1 end");
        }
    }
}