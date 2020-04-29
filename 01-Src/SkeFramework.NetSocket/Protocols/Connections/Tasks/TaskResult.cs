using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Protocols.Connections.Tasks
{
    /// <summary>
    /// 任务执行的返回结果（它只是一个数据结构）。
    /// </summary>
    [Serializable]
    public class TaskResult
    {
        /// <summary>
        /// 任务操作成功标识。
        /// </summary>
        private bool success;
        /// <summary>
        /// 任务结果描述信息。
        /// </summary>
        private string description;
        /// <summary>
        /// 任务结果编码
        /// </summary>
        private int resultCode;
        /// <summary>
        /// 任务操作完成后数据的返回。
        /// </summary>
        private object param;

        /// <summary>
        /// 任务结果编码
        /// </summary>
        public int ResultCode
        {
            get { return resultCode; }
            set { resultCode = value; }
        }
        /// <summary>
        /// 任务操作成功标识。
        /// </summary>
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }
        /// <summary>
        /// 获取或设置任务结果描述信息。
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// <summary>
        /// 任务操作完成后数据的返回。
        /// </summary>
        public object Param
        {
            get { return param; }
            set { param = value; }
        }
        public TaskResult()
        {
        }
        public TaskResult(bool success, string description, object param)
        {
            Success = success;
            Description = description;
            Param = param;
        }
        public TaskResult(bool success, ResultStatusCode status, string description, object param)
        {
            this.Success = success;
            this.resultCode = status.GetStatusCode();
            this.Description = description;
            this.Param = param;
        }

        public override string ToString()
        {
            return string.Format("TaskResult:{0};ResultCode:{1};Description:{2}", this.success, this.resultCode, this.description);
        }
    }
}
