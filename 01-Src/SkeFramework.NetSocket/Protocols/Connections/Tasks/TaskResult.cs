using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Protocols.Connections.Tasks
{
    /// <summary>
    /// 任务执行的返回结果（它只是一个数据结构）。
    /// </summary>
    [Serializable]
    public class TaskResult
    {

        /// <summary>
        /// 任务结果编码
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 任务操作成功标识。
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 获取或设置任务结果描述信息。
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 任务操作完成后数据的返回。
        /// </summary>
        public object Param { get; set; }
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
            this.ResultCode = status.GetStatusCode();
            this.Description = description;
            this.Param = param;
        }

        public override string ToString()
        {
            return string.Format("TaskResult:{0};ResultCode:{1};Description:{2}", this.Success, this.ResultCode, this.Description);
        }
    }
}
