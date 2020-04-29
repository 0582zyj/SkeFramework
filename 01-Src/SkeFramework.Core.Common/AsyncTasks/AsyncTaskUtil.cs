using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Common.AsyncTasks
{
    /// <summary>
    /// 异步任务工具栏
    /// </summary>
    public class AsyncTaskUtil
    {
        /// <summary>
        /// 开始异步任务
        /// </summary>
        /// <param name="action"></param>
        public static void StartTask(Action action)
        {
            try
            {
                Action newAction = () =>
                { };
                newAction += action;
                Task task = new Task(newAction);
                task.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
