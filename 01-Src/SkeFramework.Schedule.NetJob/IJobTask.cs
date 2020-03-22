using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob
{
     public interface IJobTask
    {
        Task<TData> Start();
    }
}
