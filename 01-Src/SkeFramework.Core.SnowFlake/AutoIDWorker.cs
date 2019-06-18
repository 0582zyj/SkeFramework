using System;

namespace SkeFramework.Core.SnowFlake
{
    /// <summary>
    /// 自动Id业务
    /// </summary>
    public class AutoIDWorker
    {

        /// <summary>
        /// 机器ID
        /// </summary>
        private static long workerId;
        /// <summary>
        /// 唯一时间，这是一个避免重复的随机量，自行设定不要大于当前时间戳
        /// </summary>
        private static readonly long twepoch = 687888001020L; 
        /// <summary>
        /// 序号
        /// </summary>
        private static long sequence = 0L;
        /// <summary>
        /// 机器码字节数。4个字节用来保存机器码(定义为Long类型会出现，最大偏移64位，所以左移64位没有意义)
        /// </summary>
        private static readonly int workerIdBits = 4;
        /// <summary>
        /// 最大机器ID
        /// </summary>
        public static long maxWorkerId = -1L ^ -1L << workerIdBits;
        /// <summary>
        /// 计数器字节数，10个字节用来保存计数码
        /// </summary>
        private static readonly int sequenceBits = 10;
        /// <summary>
        /// 机器码数据左移位数，就是后面计数器占用的位数
        /// </summary>
        private static readonly int workerIdShift = sequenceBits;
        /// <summary>
        /// 时间戳左移动位数就是机器码和计数器总字节数
        /// </summary>
        private static readonly int timestampLeftShift = sequenceBits + workerIdBits;
        /// <summary>
        /// 一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        /// </summary>
        public static long sequenceMask = -1L ^ -1L << sequenceBits; 

        private long lastTimestamp = -1L;

        /// <summary>
        /// 机器码
        /// </summary>
        /// <param name="workerId"></param>
        public AutoIDWorker(long workerId)
        {
            if (workerId > maxWorkerId || workerId < 0)
                throw new Exception(string.Format("worker Id can't be greater than {0} or less than 0 ", workerId));
            AutoIDWorker.workerId = workerId;
        }
        /// <summary>
        /// 获取下个自动ID
        /// </summary>
        /// <returns></returns>
        public long GetAutoID()
        {        
            //你问他房子总价贵吗
            lock (this)
            {
                long timestamp = TimeGen();
                if (this.lastTimestamp == timestamp)
                { //同一微妙中生成ID
                    AutoIDWorker.sequence = (AutoIDWorker.sequence + 1) & AutoIDWorker.sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                    if (AutoIDWorker.sequence == 0)
                    {
                        //一微妙内产生的ID计数已达上限，等待下一微妙
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
                long nextId = (timestamp - twepoch << timestampLeftShift) | AutoIDWorker.workerId << AutoIDWorker.workerIdShift | AutoIDWorker.sequence;
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
