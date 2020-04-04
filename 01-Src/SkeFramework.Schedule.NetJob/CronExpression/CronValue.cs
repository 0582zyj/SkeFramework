using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.CronExpression
{
    abstract class CrontabValueTimeGroup
    {
        public abstract bool Check(DateTime time, out bool next);
    }

    abstract class CrontabValueDateGroup
    {
        public abstract bool Check(DateTime time, bool useNext);
    }

    class CrontabValueHourMinuteGroup : CrontabValueTimeGroup
    {
        private readonly CronMinuteValue minute;
        private readonly CronHourValue hour;

        public CrontabValueHourMinuteGroup(CronMinuteValue minute, CronHourValue hour)
        {
            this.minute = minute;
            this.hour = hour;
        }

        public override bool Check(DateTime time, out bool next)
        {
            bool useNext = false;
            next = false;
            if (minute != null)
            {
                if (!minute.Check(time, out next))
                {
                    return false;
                }
                useNext = next;
            }
            if (hour != null)
            {
                if (!hour.Check(time, useNext, out next))
                {
                    return false;
                }
            }
            return true;
        }
    }

    class CrontabValueTimeRangeGroup : CrontabValueTimeGroup
    {
        private readonly CronTimeRangeValue timeRange;

        public CrontabValueTimeRangeGroup(CronTimeRangeValue timeRange)
        {
            this.timeRange = timeRange;
        }

        public override bool Check(DateTime time, out bool next)
        {
            next = false;
            if (timeRange != null)
            {
                if (!timeRange.Check(time, out next))
                {
                    return false;
                }
            }
            return true;
        }
    }

    class CrontabValueMonthDayGroup : CrontabValueDateGroup
    {
        private readonly CronDayValue day;
        private readonly CronMonthValue month;

        public CrontabValueMonthDayGroup(CronDayValue day, CronMonthValue month)
        {
            this.day = day;
            this.month = month;
        }

        public override bool Check(DateTime time, bool useNext)
        {
            if (day != null)
            {
                if (!day.Check(time, useNext, out bool next))
                {
                    return false;
                }
                useNext = next;
            }
            else
            {
                useNext = false;
            }
            if (month != null)
            {
                if (!month.Check(time, useNext))
                {
                    return false;
                }
            }
            return true;
        }
    }

}