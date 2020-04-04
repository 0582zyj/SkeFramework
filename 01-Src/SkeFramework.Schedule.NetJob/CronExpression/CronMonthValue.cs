using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkeFramework.Schedule.NetJob.CronExpression
{
     
     class CronMonthValue
    {
        static readonly char[] Separator = { ',' };

        static readonly Regex FromToRegex = new Regex(@"^(?<start>\d+)-(?<end>\d+)(/(?<interval>\d+))?$");

        static readonly Regex AllFromToRegex = new Regex(@"^\*/(?<interval>\d+)$");

        const int min = 1;
        const int max = 12;
        readonly List<CronNodeBase> list;

        public static bool TryParse(string value, out CronMonthValue crontabValue)
        {
            crontabValue = null;
            var list = new List<CronNodeBase>();
            var array = value.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array)
            {
                if (item == "*" || item == "*/1")
                {
                    crontabValue = new CronMonthValue(null);
                    return true;
                }
                if (int.TryParse(item, out int data))
                {
                    if (data >= min && data <= max)
                    {
                        list.Add(new SingleCrontabValueNode(data));
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                var aftmatch = AllFromToRegex.Match(item);
                if (aftmatch.Success)
                {
                    var interval = Convert.ToInt32(aftmatch.Groups["interval"].Value);
                    if (interval > 0)
                    {
                        list.Add(new RangeCrontabValueNode(min, max, min, max, interval));
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                var ftmatch = FromToRegex.Match(item);
                if (ftmatch.Success)
                {
                    var start = Convert.ToInt32(ftmatch.Groups["start"].Value);
                    var end = Convert.ToInt32(ftmatch.Groups["end"].Value);
                    var interval = 1;
                    if (ftmatch.Groups["interval"].Success)
                    {
                        interval = Convert.ToInt32(ftmatch.Groups["interval"].Value);
                    }
                    if (start >= min && start <= max && end >= min && end <= max && interval > 0)
                    {
                        list.Add(new RangeCrontabValueNode(min, max, start, end, interval));
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            if (list.Count == 0)
            {
                return false;
            }
            else
            {
                crontabValue = new CronMonthValue(list);
                return true;
            }
        }

        CronMonthValue(List<CronNodeBase> list)
        {
            this.list = list;
        }

        // public override bool AllowNext => true;

        public bool Check(DateTime time, bool useNext)
        {
            if (list == null)
            {
                return true;
            }
            else
            {
                if (useNext)
                {
                    time = time.AddMonths(-1);
                }
                var value = time.Month;
                foreach (var item in list)
                {
                    if (item.Check(value, out bool next))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
