﻿using SkeFramework.NetSerialPort.Protocols.Requests;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Protocols.Connections.Tasks
{
    public class TaskManager
    {
        /// <summary>
        /// 任务列表。
        /// </summary>
        private readonly ConcurrentStack<ConnectionTask> taskList = new ConcurrentStack<ConnectionTask>();
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
                result= taskList.TryPop(out task);
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
                    if (task.GetRelatedProtocol() == null)
                    {
                        task.SetRelatedProtocol(this.relatedProtocol);
                    }
                    taskList.Push(task);
                    //((EventWaitHandle)this.relatedProtocol.protocolEvents[(int)ProtocolEvents.TaskArrived]).Set();
                    string LogMsg = string.Format("协议“{0}”增加了一个“{1}”任务。", "未定义协议名称", task.Name);
                }
            }
        }
        /// <summary>
        /// 检查协议关联的任务超时
        /// </summary>
        /// <param name="connection"></param>
        public void SetTaskTimeout(IConnection connection)
        {
            if (connection == null || TaskList.Count == 0)
            {
                return;
            }
            foreach (var task in TaskList.Where(o => o.GetRelatedProtocol() != null && o.GetRelatedProtocol().ControlCode == connection.ControlCode))
            {
                if(connection is RefactorRequestChannel)
                {
                    RefactorRequestChannel requestChannel = (RefactorRequestChannel)connection;
                    if (DateTime.Now.Subtract(task.ProcessingTime).TotalMilliseconds > requestChannel.Sender.TimeoutMS && requestChannel.Sender.TimeoutMS>0)
                    {
                        task.SetAsOvertime();
                    }
                }
               
            }
        }
    }
}
