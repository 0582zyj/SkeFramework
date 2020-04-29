using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.DataAttributes
{
    /// <summary>
    /// 指定调度的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ScheduleAttribute : Attribute
    {
        /// <summary>
        /// 时间表值
        /// </summary>
        public string Schedule { get; }
        /// <summary>
        /// 时间表名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="schedule"></param>
        public ScheduleAttribute(string name, string schedule)
        {
            Name = name;
            Schedule = schedule;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="schedule"></param>
        public ScheduleAttribute(string schedule)
        {
            Schedule = schedule;
        }

        /// <summary>
        /// 是否允许并发执行
        /// </summary>
        public bool AllowConcurrentExecution { get; set; } = false;
        /// <summary>
        /// 是否立即执行
        /// </summary>
        public bool RunImmediately { get; set; } = false;
        /// <summary>
        /// 是否执行
        /// </summary>
        public bool AutoEnable { get; set; } = true;
    }
}