using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Protocols.Connections.Tasks
{
    public class TaskManager
    {
        /// <summary>
        /// 任务列表。
        /// </summary>
        private readonly List<ConnectionTask> taskList = new List<ConnectionTask>();
        /// <summary>
        /// 获取或设置任务关联的协议。
        /// </summary>
        [NonSerialized()]
        private IConnection relatedProtocol = null;
        /// <summary>
        /// 获取任务列表
        /// </summary>
        public TaskManager(IConnection protocol)
        {
            this.relatedProtocol = protocol;
        }

        /// <summary>
        /// 获取任务列表。
        /// 返回值为只读列表。
        /// </summary>
        public IList<ConnectionTask> TaskList
        {
            get
            {
                lock (taskList)
                {
                    // 这里以只读的方式返回了taskList在此时间片的一个副本。
                    // 因为有多个线程会访问它，但又不想在协议线程中要lock（taksList）而增加了任务操作的负责度和更多的错误源。
                    List<ConnectionTask> snapshot = new List<ConnectionTask>();
                    snapshot.AddRange(taskList);
                    return snapshot.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// 移除一个任务
        /// </summary>
        /// <param name="task">被移除的任务</param>
        internal bool RemoveTask(ConnectionTask task)
        {
            bool result;
            lock (taskList)
            {
                result = taskList.Remove(task);
            }
            if (result)
            {
                string LogMsg = string.Format("协议“{0}”中“{1}”任务已结束。", task.GetRelatedProtocol().Local.ToString(), task.Name);
                Console.WriteLine(LogMsg);
            }
            return result;
        }
        /// <summary>
        /// 给规约增加任务
        /// </summary>
        /// <param name="task">要添加的任务。</param>
        internal void AddTask(ConnectionTask task)
        {
            if (task == null)
                return;
            lock (taskList)
            {
                if (!taskList.Contains(task))
                {
                    task.SetRelatedProtocol(this.relatedProtocol);
                    taskList.Insert(0, task);
                    //((EventWaitHandle)this.relatedProtocol.protocolEvents[(int)ProtocolEvents.TaskArrived]).Set();
                    string LogMsg = string.Format("协议“{0}”增加了一个“{1}”任务。", "未定义协议名称", task.Name);
                }
            }
        }
    }
}
