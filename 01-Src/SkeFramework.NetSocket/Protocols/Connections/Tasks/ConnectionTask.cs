using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkeFramework.NetSerialPort.Protocols.Connections.Tasks;
using SkeFramework.NetSerialPort.Protocols.Constants;

namespace SkeFramework.NetSerialPort.Protocols.Connections
{
    /// <summary>
    /// 应用发给规约的任务（它只是一个数据结构）。
    /// </summary>
    [Serializable]
    public class ConnectionTask
    {
        #region 私有属性
        /// <summary>
        /// -1表示无限时。
        /// </summary>
        private int asyncTaskTimeouts = -1;
        [NonSerialized()]
        private AsyncTaskResult asyncTaskResult;
        [NonSerialized()]
        private Stopwatch asyncTaskStopwatch;
        /// <summary>
        /// 获取或设置任务关联的协议。
        /// </summary>
        [NonSerialized()]
        private IConnection relatedConnection = null;
        #endregion

        #region 公开属性
        /// <summary>
        /// 任务是否过期
        /// </summary>
        public bool Dead { get; set; } = false;
        /// <summary>
        /// 获取或设置任务名。
        /// 注：它是对任务的描述。对于它的意义，主要看应用开发和协议开发之间的协商和业务的需要。
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 获取或设置任务参数。
        /// 注：它是object类型，用来传递数据给协议。对于它的意义，主要看应用开发和协议开发之间的协商和业务的需要。
        /// </summary>
        public object Param { get; set; }
        /// <summary>
        /// 获取或设置任务的执行结果。
        /// </summary>
        public TaskResult Result { get; set; }
        /// <summary>
        /// 获取任务状态。
        /// </summary>
        public TaskState TaskState { get; private set; }
        /// <summary>
        /// 任务运行开始时间
        /// </summary>
        public DateTime ProcessingTime { get; private set; }
        [JsonIgnore]
        public CancellationToken cancellationToken { get; private set; }
        #endregion

        /// <summary>
        /// 实例化协议任务。
        /// </summary>
        public ConnectionTask()
        {
        }
        /// <summary>
        /// 实例化协议任务。
        /// </summary>
        /// <param name="name">任务名。</param>
        /// <param name="param">任务参数。</param>
        public ConnectionTask(string name, object param)
        {
            Name = name;
            Param = param;
            TaskState = TaskState.NewTask;
            cancellationToken = new CancellationToken();
        }

        #region 任务状态：新任务》处理中》完成/超时
        /// <summary>
        /// 任务完成。
        /// </summary>
        public void Complete()
        {
            // 若任务未经过协议的ExecuteTask来执行，则relatedProtocol为空
            if (relatedConnection == null)
            {
                Complete(TaskState.Completed);
            }
            else
            {
                // 如果任务已经被删除，说明任务的执行时间已经超时并已经通知了应用程序，
                // 因此此时再次通知任务完成实际上是没意义的。
                Complete(TaskState.Completed);
                relatedConnection.StopReceive();
            }
        }
        /// <summary>
        /// 通知任务已经处于处理状态。
        /// </summary>
        public void SetAsBeProccessing()
        {
            if (TaskState == TaskState.NewTask)
                TaskState = TaskState.Processing;
            this.ProcessingTime = DateTime.Now;
        }
        /// <summary>
        /// 设置任务超时。
        /// </summary>
        public void SetAsOvertime()
        {
            TaskState = TaskState.TaskOvertime;
            this.Dead = true;
        }
        #endregion

        #region internal访问域函数
        /// <summary>
        /// 设置关联协议
        /// </summary>
        /// <param name="connection"></param>
        public void SetRelatedProtocol(IConnection connection)
        {
            //connection.Local.TaskTag = this.name;
            if (connection.RemoteHost != null)
            {
                connection.RemoteHost.TaskTag = this.Name;
                connection.Receiving = true;
            }
            relatedConnection = connection;
        }
        /// <summary>
        /// 获取关联协议
        /// </summary>
        /// <returns></returns>
        internal IConnection GetRelatedProtocol()
        {
            return relatedConnection;
        }

        /// <summary>
        /// 初始化同步对象
        /// </summary>
        internal void InitSynchObject()
        {
            if (asyncTaskResult == null)
                asyncTaskResult = new AsyncTaskResult(null, null);
        }

        /// <summary>
        /// 函数将阻塞当前线程，直至任务完成。
        /// 另一个线程将调用SetAsCompleted函数通知任务完成以解除线程阻塞。
        /// </summary>
        /// <param name="timeout">等待的毫秒数（-1表示无限期等待）。</param>
        internal void WaitBeCompleted(int timeout)
        {
            if (asyncTaskResult == null)
                return;
            //throw new Exception("在执行同步等待前需先初始化同步对象。");
            asyncTaskResult.EndInvoke(timeout);    // 等待同步完成。
        }

        /// <summary>
        /// 设置任务完成。
        /// 若当前线程因为调用了WaitToComplete函数而阻塞，那么此函数将使当前线程恢复运行。
        /// </summary>
        /// <param name="tkState">任务状态。</param>
        public void Complete(TaskState tkState)
        {
            TaskState = tkState;
            if (asyncTaskResult != null)
                asyncTaskResult.SetAsCompleted(true);
        }

        /// <summary>
        /// 设置异步调用的回调函数和传递的对象。
        /// </summary>
        /// <param name="asyncCallback">异步回调对象</param>
        /// <param name="state">传递的对象</param>
        /// <param name="timeout">执行任务的时限毫秒（值为-1表示无限时），若超时将调用asyncCallback的回调函数。</param>
        internal void AsyncInvokeSetup(AsyncCallback asyncCallback, object state, int timeout)
        {
            // 添加后面这个判断是怕应用程序开发员将一个已经执行了的异步任务再此执行异步任务，并更改了回调函数。
            if (asyncTaskResult == null || !asyncTaskResult.AsyncCallbackObjEquals(asyncCallback))
                asyncTaskResult = new AsyncTaskResult(asyncCallback, state);

            asyncTaskTimeouts = timeout;
            //Hungry mode
            if (NetworkConstants.WAIT_FOR_COMPLETE != asyncTaskTimeouts)
            {
                asyncTaskStopwatch = Stopwatch.StartNew();
            }
            else
            {
                asyncTaskStopwatch = null;
            }
        }

        /// <summary>
        /// 返回异步任务执行是否超时。
        /// </summary>
        internal bool AsyncTaskOvertime
        {
            get
            {
                if (asyncTaskStopwatch != null && asyncTaskTimeouts >= 0)
                    return asyncTaskStopwatch.ElapsedMilliseconds >= asyncTaskTimeouts;
                return false;
            }
        }

        #endregion

        public override string ToString()
        {
            var jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(this, jSetting);
        }
    }

    /// <summary>
    /// 任务状态。
    /// </summary>
    [Serializable]
    public enum TaskState
    {
        /// <summary>
        /// 一个刚收到的新任务（即还未做任何处理状态）。
        /// </summary>
        NewTask,
        /// <summary>
        /// 任务正在处理中。
        /// </summary>
        Processing,
        /// <summary>
        /// 任务已完成。
        /// </summary>
        Completed,
        /// <summary>
        /// 在指定的时间内未完成任务的执行。
        /// </summary>
        TaskOvertime
    }
}
