using SkeFramework.Schedule.NetJob.CronExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.DataHandle
{
     public class JobSchedule
    {
        static readonly char[] Separator = { ' ' };

        /// <summary>
        /// Try to parse the value for the schedule
        /// </summary>
        /// <param name="value"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public static bool TryParse(string value, out JobSchedule schedule)
        {
            schedule = null;
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            string[] array = value.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 0)
            {
                return false;
            }
            var v1 = array[0];
            if (CronMinuteValue.TryParse(v1, out CronMinuteValue minute))
            {
                CronHourValue hour = null;
                CronDayValue day = null;
                CronMonthValue month = null;
                CronWeekValue week = null;
                if (array.Length >= 2)
                {
                    if (!CronHourValue.TryParse(array[1], out hour))
                    {
                        return false;
                    }
                }
                if (array.Length >= 3)
                {
                    if (!CronDayValue.TryParse(array[2], out day))
                    {
                        return false;
                    }
                }
                if (array.Length >= 4)
                {
                    if (!CronMonthValue.TryParse(array[3], out month))
                    {
                        return false;
                    }
                }
                if (array.Length >= 5)
                {
                    if (!CronWeekValue.TryParse(array[4], out week))
                    {
                        return false;
                    }
                }
                if (array.Length >= 6)
                {
                    return false;
                }
                var timeGroup = new CrontabValueHourMinuteGroup(minute, hour);
                var dateGroup = new CrontabValueMonthDayGroup(day, month);
                schedule = new JobSchedule(value, timeGroup, dateGroup, week);
                return true;
            }
            else if (CronTimeRangeValue.TryParse(v1, out CronTimeRangeValue timeRange))
            {
                CronDayValue day = null;
                CronMonthValue month = null;
                CronWeekValue week = null;
                if (array.Length >= 2)
                {
                    if (!CronDayValue.TryParse(array[1], out day))
                    {
                        return false;
                    }
                }
                if (array.Length >= 3)
                {
                    if (!CronMonthValue.TryParse(array[2], out month))
                    {
                        return false;
                    }
                }
                if (array.Length >= 4)
                {
                    if (!CronWeekValue.TryParse(array[3], out week))
                    {
                        return false;
                    }
                }
                if (array.Length >= 5)
                {
                    return false;
                }
                var timeGroup = new CrontabValueTimeRangeGroup(timeRange);
                var dateGroup = new CrontabValueMonthDayGroup(day, month);
                schedule = new JobSchedule(value, timeGroup, dateGroup, week);
                return true;
            }
            else
            {
                return false;
            }
        }





        /// <summary>
        /// The schedule value
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Check the time in the schedule
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool Check(DateTime time)
        {
            if (!timeGroup.Check(time, out bool next))
            {
                return false;
            }
            if (!dateGroup.Check(time, next))
            {
                return false;
            }
            if (week != null)
            {
                if (!week.Check(time, next))
                {
                    return false;
                }
            }
            return true;
        }

        private readonly CrontabValueTimeGroup timeGroup;
        private readonly CrontabValueDateGroup dateGroup;
        private readonly CronWeekValue week;

        JobSchedule(string value, CrontabValueTimeGroup timeGroup, CrontabValueDateGroup dateGroup, CronWeekValue week)
        {
            this.timeGroup = timeGroup;
            this.dateGroup = dateGroup;
            this.week = week;
            this.Value = value;
        }
    }
}
