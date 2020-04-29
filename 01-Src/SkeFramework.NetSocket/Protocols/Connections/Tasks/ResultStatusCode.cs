using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Protocols.Connections.Tasks
{
    /// <summary>
    /// 协议处理结果
    /// </summary>
    public class ResultStatusCode
    {
        public static ResultStatusCode FAIL = new ResultStatusCode(0, "抱歉，任务处理失败!");
        public static ResultStatusCode SUCCESS = new ResultStatusCode(1, "处理成功!");
        public static ResultStatusCode TIME_OUT = new ResultStatusCode(2, "抱歉，任务处理失败，处理超时!");
        public static ResultStatusCode CFGCHANG = new ResultStatusCode(8, "配置发送变化");
        public static ResultStatusCode NOLOGIN = new ResultStatusCode(9, "未登录");
        public static ResultStatusCode NEW_TASK_COMING = new ResultStatusCode(10, "新任务挤出旧任务");

        /// <summary>
        /// 状态码
        /// </summary>
        private readonly int _statusCode;
        /// <summary>
        /// 描述
        /// </summary>
        private string _desc;
        /// <summary>
        /// 消息处理结果状态码
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="desc"></param>
        private ResultStatusCode(int statusCode, string desc)
        {
            this._statusCode = statusCode;
            this._desc = desc;
        }
        /// <summary>
        /// 消息处理结果状态码
        /// </summary>
        /// <returns></returns>
        public int GetStatusCode()
        {
            return _statusCode;
        }
        /// <summary>
        /// 获取消息处理结果状态码对应的描述信息
        /// </summary>
        /// <returns></returns>
        public string GetDesc()
        {
            return _desc;
        }
        /// <summary>
        /// 设置消息处理结果状态码的描述信息
        /// </summary>
        /// <param name="desc"></param>
        public void SetDesc(string desc)
        {
            this._desc = desc;
        }
    }
}

