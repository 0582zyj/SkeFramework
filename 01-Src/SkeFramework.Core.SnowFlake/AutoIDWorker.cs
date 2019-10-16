using System;
using System.Threading.Tasks;

/// <summary>
/// 分布式雪花ID
/// 0 - 0000000000 0000000000 0000000000 0000000000 0 - 00000 - 00000 - 000000000000
/// 1位标识部分，在java中由于long的最高位是符号位，正数是0，负数是1，一般生成的ID为正数，所以为0；
/// 41位时间戳部分，这个是毫秒级的时间，一般实现上不会存储当前的时间戳，而是时间戳的差值（当前时间-固定的开始时间），这样可以使产生的ID从更小值开始；41位的时间戳可以使用69年，(1L << 41) / (1000L * 60 * 60 * 24 * 365) = 69年；
/// 10位节点部分，Twitter实现中使用前5位作为数据中心标识，后5位作为机器标识，可以部署1024个节点；
/// 12位序列号部分，支持同一毫秒内同一个节点可以生成4096个ID；
/// </summary>
namespace SkeFramework.Core.SnowFlake
{
    /// <summary>
    /// 自动Id业务
    /// </summary>
    public class AutoIDWorker
    {
        #region 单例
        /// <summary>
        /// 静态实例
        /// </summary>
        private static AutoIDWorker instance;
        public static AutoIDWorker Example
        {
            get
            {
                if (AutoIDWorker.instance == null)
                {
                    AutoIDWorker.instance = new AutoIDWorker(1);
                }
                return instance;
            }
        }
        #endregion
        /// <summary>
        /// 机器ID
        /// </summary>
        private static long workerId;
        /// <summary>
        /// 序号
        /// </summary>
        private static long sequence = 0L;
        /// <summary>
        /// 上一计时
        /// </summary>
        private long lastTimestamp = -1L;
        /// <summary>
        /// 起始的时间戳，这是一个避免重复的随机量，自行设定不要大于当前时间戳
        /// </summary>
        private const long twepoch = 687888001020L;


        #region 每一部分向左的位移
        /** 数据中心ID(0~31) */
        private const int datacenterId = 5;
        /// <summary>
        /// 机器码字节数。4个字节用来保存机器码(定义为Long类型会出现，最大偏移64位，所以左移64位没有意义)
        /// </summary>
        private const int workerIdBits = 5;
        /// <summary>
        /// 计数器字节数，用来保存计数码
        /// </summary>
        private static readonly int sequenceBits = 12;
        /// <summary>
        /// 机器码数据左移位数，就是后面计数器占用的位数
        /// </summary>
        private static readonly int workerIdShift = sequenceBits;
        /// <summary>
        /// 机器码数据左移位数，就是后面计数器占用的位数
        /// </summary>
        private static readonly int datacenterIdShift = workerIdShift + workerIdBits;
        /// <summary>
        /// 时间戳左移动位数就是机器码和计数器总字节数
        /// </summary>
        private static readonly int timestampLeftShift = datacenterIdShift + datacenterId;
        #endregion

        #region 每一部分的最大值
        /// <summary>
        /// 一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        /// </summary>
        public static long sequenceMask = -1L ^ -1L << sequenceBits;
        /// <summary>
        /// 最大机器ID
        /// </summary>
        public static long maxWorkerId = -1L ^ -1L << workerIdBits;
        #endregion


        /// <summary>
        /// 机器码        
        /// </summary>
        /// <param name="workerId"></param>
        public AutoIDWorker(long workerId)
        {
            Console.WriteLine(@"+------+----------------------+----------------+-----------+");
            Console.WriteLine(@"| sign |     delta seconds    | worker node id | sequence  |");
            Console.WriteLine(@"+------+----------------------+----------------+-----------+");
            Console.WriteLine(@"  1bit          41bits              10bits         10bits");
            if (workerId > maxWorkerId || workerId < 0)
                throw new Exception(string.Format("worker Id {0} can't be greater than {1} or less than 0 ", workerId, maxWorkerId));
            AutoIDWorker.workerId = workerId;
        }
        /// <summary>
        /// 获取下个自动ID
        /// </summary>
        /// <returns></returns>
        public long GetAutoID()
        {
            //加个锁
            lock (this)
            {
                long timestamp = TimeGen();
                if (this.lastTimestamp == timestamp)
                { //同一微妙中生成ID
                  //用&运算计算该微秒内产生的计数是否已经到达上限
                    AutoIDWorker.sequence = (AutoIDWorker.sequence + 1) & AutoIDWorker.sequenceMask;
                    if (AutoIDWorker.sequence == 0)
                    {//一微妙内产生的ID计数已达上限，等待下一微妙
                        timestamp = TillNextMillis(this.lastTimestamp);
                    }
                }
                else
                { //不同微秒生成ID
                    AutoIDWorker.sequence = 0; //计数清0
                }
                if (timestamp < lastTimestamp)
                { //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
                    throw new Exception(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                    this.lastTimestamp - timestamp));
                }
                this.lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
                long nextId = (timestamp - twepoch << timestampLeftShift)
                | AutoIDWorker.datacenterId << AutoIDWorker.workerIdShift   //数据中心部分 
                | AutoIDWorker.workerId << AutoIDWorker.workerIdShift  //机器标识部分
                | AutoIDWorker.sequence; //序列号部分
                return nextId;
            }
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long TillNextMillis(long lastTimestamp)
        {
            long timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }
        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private long TimeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0,
                DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
