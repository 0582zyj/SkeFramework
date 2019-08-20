using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Common.Validates;

namespace SkeFramework.Core.Common.Randoms
{
    /// <summary>
    /// 随机数工具类
    /// </summary>
    public class RandomUtils
    {
        private static Random RANDOM = new Random();


        public RandomUtils()
        {
        }

        public static int NextInt()
        {
            return NextInt(0, int.MaxValue);
        }
        public static long NextLong()
        {
            return NextLong(0L, 9223372036854775807L);
        }
        public static double NextDouble()
        {
            return NextDouble(0.0D, 1.7976931348623157E308D);
        }

        public static bool NextBoolean()
        {
            return RANDOM.Next(0, 1) == 1 ? true : false;
        }

        public static byte[] NextBytes(int count)
        {
            //ValidateUtils.isTrue(count >= 0, "Count cannot be negative.", new Object[0]);
            byte[] result = new byte[count];
            RANDOM.NextBytes(result);
            return result;
        }
        /// <summary>
        /// 获取Int类型随机数
        /// </summary>
        /// <param name="startInclusive"></param>
        /// <param name="endExclusive"></param>
        /// <returns></returns>
        public static int NextInt(int startInclusive, int endExclusive)
        {
            //ValidateUtils.isTrue(endExclusive >= startInclusive, "Start value must be smaller or equal to end value.", new Object[0]);
            //ValidateUtils.isTrue(startInclusive >= 0, "Both range values must be non-negative.", new Object[0]);
            return startInclusive == endExclusive ? startInclusive : startInclusive + RANDOM.Next(endExclusive - startInclusive);
        }

        public static long NextLong(long startInclusive, long endExclusive)
        {
            //ValidateUtils.isTrue(endExclusive >= startInclusive, "Start value must be smaller or equal to end value.", new Object[0]);
            //ValidateUtils.isTrue(startInclusive >= 0L, "Both range values must be non-negative.", new Object[0]);
            return startInclusive == endExclusive ? startInclusive : (long)NextDouble((double)startInclusive, (double)endExclusive);
        }

        public static double NextDouble(double startInclusive, double endInclusive)
        {
            //ValidateUtils.isTrue(endInclusive >= startInclusive, "Start value must be smaller or equal to end value.", new Object[0]);
            //ValidateUtils.isTrue(startInclusive >= 0.0D, "Both range values must be non-negative.", new Object[0]);
            return startInclusive == endInclusive ? startInclusive : startInclusive + (endInclusive - startInclusive) * RANDOM.NextDouble();
        }

    }
}
