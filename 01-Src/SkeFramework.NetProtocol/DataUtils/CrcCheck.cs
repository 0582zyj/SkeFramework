using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULCloudLockTool.BLL.SHProtocol.DataFrame.DataUtil
{
    /// <summary>
    /// CRC校验通用计算类，可计算8位、16位或32位CRC校验值
    /// </summary>
    public class CRCValue
    {
        const int MaxByteValues = 256;
        const int BitSperByte = 8;
        public System.UInt32[] bits =  {
                                           0x00000001, 0x00000002, 0x00000004, 0x00000008,
                                           0x00000010, 0x00000020, 0x00000040, 0x00000080,
                                           0x00000100, 0x00000200, 0x00000400, 0x00000800,
                                           0x00001000, 0x00002000, 0x00004000, 0x00008000,
                                           0x00010000, 0x00020000, 0x00040000, 0x00080000,
                                           0x00100000, 0x00200000, 0x00400000, 0x00800000,
                                           0x01000000, 0x02000000, 0x04000000, 0x08000000,
                                           0x10000000, 0x20000000, 0x40000000, 0x80000000,
        };

        System.UInt32[] values = new System.UInt32[MaxByteValues];

        int BITCOUNT;
        System.UInt32 POLYNOMINAL;
        bool FREVERSE;
        System.UInt32 INITIAL;
        System.UInt32 FINALMASK;
        System.UInt32 mask;
        System.UInt32 crc_register;

        /// <summary>
        /// 获取bits.
        /// </summary>
        public System.UInt32[] Bits
        {
            get { return bits; }
        }

        /// <summary>
        /// CRC校验计算
        /// </summary>
        /// <param name="BitCount">位数(8、16、32)</param>
        /// <param name="Polynominal">多项式</param>
        /// <param name="ShiftRight">是否右移</param>
        /// <param name="Initial">初始值</param>
        /// <param name="FinalMask">最终掩码</param>
        public CRCValue(int BitCount, System.UInt32 Polynominal, bool ShiftRight,
            System.UInt32 Initial, System.UInt32 FinalMask)
        {
            // BitCount    : CRC Size
            // Polynominal : CRC Polynomial
            // Reverse     : Reversed (means shift right)
            // Initial     : Initial CRC Register Value
            // FinalMask   : Final CRC XOR Value
            BITCOUNT = BitCount;
            POLYNOMINAL = Polynominal;
            INITIAL = Initial;
            FINALMASK = FinalMask;
            // Mask needed to mask off redundent bits
            System.UInt32 l = 1;
            mask = ((l << (BITCOUNT - 1)) - 1) | (l << (BITCOUNT - 1));
            REVERSE = ShiftRight;        // Before set this property
            // mask must be calculated
        }

        bool REVERSE
        {
            get
            {
                return FREVERSE;
            }
            set
            {

                FREVERSE = value;
                if (FREVERSE)
                {
                    for (System.UInt32 ii = 0; ii < MaxByteValues; ++ii)
                        values[ii] = ReverseTableEntry(ii);
                }
                else
                {
                    for (UInt32 ii = 0; ii < MaxByteValues; ++ii)
                        values[ii] = ForwardTableEntry(ii);
                }

            }
        }

        System.UInt32 Reverse(System.UInt32 value)
        {
            //    This function returns the reversed bit patter from its input.
            //    For example, 1010 becomes 0101.
            //
            //  Parameters:
            //
            //    value: The value to reverse
            System.UInt32 result = 0;

            for (int jj = 0; jj < BITCOUNT; ++jj)
            {
                if ((value & bits[jj]) != 0)
                    result |= bits[BITCOUNT - jj - 1];
            }
            return result;
        }

        System.UInt32 ForwardTableEntry(System.UInt32 entryindex)
        {
            //    This function creates a CRC table entry for a non-reversed
            //    CRC function.
            //
            //  Parameters:
            //
            //      entryindex: The index of the CRC table entry.
            //
            //  Return Value:
            //
            //      The value for the specified CRC table entry.
            //

            System.UInt32 result = entryindex << (BITCOUNT - BitSperByte);
            for (int ii = 0; ii < BitSperByte; ++ii)
            {
                if ((result & bits[BITCOUNT - 1]) == 0)
                    result <<= 1;
                else
                    result = (result << 1) ^ POLYNOMINAL;
            }
            result &= mask;
            return result;
        }

        System.UInt32 ReverseTableEntry(System.UInt32 entryindex)
        {
            //    This function creates a CRC table entry for a reversed
            //    CRC function.
            //
            //  Parameters:
            //
            //      entryindex: The index of the CRC table entry.
            //
            //  Return Value:
            //
            //      The value for the specified CRC table entry.
            //
            System.UInt32 result = entryindex;
            for (int ii = 0; ii < BitSperByte; ++ii)
            {
                if ((result & 1) == 0)
                    result >>= 1;
                else
                    result = (result >> 1) ^ Reverse(POLYNOMINAL);
            }
            result &= mask;
            return result;
        }

        void reset()
        {
            crc_register = INITIAL;
        }

        uint value()
        {
            uint result = crc_register ^ FINALMASK;
            result &= mask;
            return result;
        }

        void update(byte[] buffer, int Start, int length)
        {
            //    This function updates the value of the CRC register based upon
            //    the contents of a buffer.
            //
            //  Parameters:
            //
            //    buffer: The input buffer
            //    length: The length of the input buffer.
            //
            // The process for updating depends upon whether or not we are using
            // the reversed CRC form.
            if (REVERSE)
            {
                for (int ii = Start; ii < length + Start; ++ii)
                {
                    crc_register = values[(crc_register ^ buffer[ii]) & 0xFF]
                        ^ (crc_register >> 8);
                }
            }
            else
            {
                for (int ii = Start; ii < length + Start; ++ii)
                {
                    System.UInt32 index = ((crc_register >> (BITCOUNT - BitSperByte)) ^ buffer[ii]);
                    crc_register = values[index & 0xFF] ^ (crc_register << BitSperByte);
                }
            }
        }

        public System.UInt32 GetCrc32(byte[] buffer, int Start, int length)
        {
            reset();
            update(buffer, Start, length);
            return value();
        }

        public System.UInt32 GetCrc(byte[] buffer, int Start, int length)
        {
            return GetCrc32(buffer, Start, length);
        }

        public System.UInt16 GetCrc16(byte[] buffer, int Start, int length)
        {
            return (System.UInt16)GetCrc32(buffer, Start, length);
        }

        public System.Byte GetCrc8(byte[] buffer, int Start, int length)
        {
            return (System.Byte)GetCrc32(buffer, Start, length);
        }
    }

    public abstract class CrcCheckBase
    {
        /// <summary>
        /// 对数据checkData进行校验。数据符合校验计算，则返回true，否则返回false.
        /// </summary>
        /// <param name="checkData">被校验的一个完整的帧。</param>
        public abstract bool Check(byte[] checkData);
        /// <summary>
        /// 计算数据的校验码，将校验码保存到目标数据checkData中。获取成功则返回true.
        /// </summary>
        /// <param name="checkData">被校验的数据帧</param>
        public abstract bool GetCheckCode(ref byte[] checkData);
    }

    // A class ===================================================================
    /// <summary>
    /// CRC校验类，获得或检查CRC的校验值。
    /// </summary>
    public class CrcCheck : CrcCheckBase
    {
        #region Fileds      *************************************************

        CRCValue crcValue = null;

        int BitCount = 0;
        uint Polynominal = 0;
        bool ShiftRight = false;
        uint Initial = 0;
        uint FinalMask = 0x0;

        int start = 4;

        #endregion

        #region Operations  *************************************************

        /// <summary>
        /// 实例化一个校验类
        /// </summary>
        public CrcCheck()
        {
        }

        /// <summary>
        /// 实例化一个CRC校验对象。注意参数不能错误。默认开始的检验是从第四位开始校验
        /// </summary>
        /// <param name="bitCount">位长</param>
        /// <param name="polynominal">多项式</param>
        /// <param name="shiftRight">右移</param>
        /// <param name="initial">初始值</param>
        /// <param name="finalMask">掩码</param>
        public CrcCheck(int bitCount, uint polynominal, bool shiftRight, uint initial, uint finalMask)
        {
            BitCount = bitCount;
            Polynominal = polynominal;
            ShiftRight = shiftRight;
            Initial = initial;
            FinalMask = finalMask;

            crcValue = new CRCValue(BitCount, Polynominal, ShiftRight, Initial, FinalMask);
        }
        /// <summary>
        /// 实例化一个CRC校验对象。注意参数不能错误。
        /// </summary>
        /// <param name="bitCount">位长</param>
        /// <param name="polynominal">多项式</param>
        /// <param name="shiftRight">右移</param>
        /// <param name="initial">初始值</param>
        /// <param name="finalMask">掩码</param>
        public CrcCheck(int bitCount, uint polynominal, bool shiftRight, uint initial, uint finalMask, int startCheck)
        {
            BitCount = bitCount;
            Polynominal = polynominal;
            ShiftRight = shiftRight;
            Initial = initial;
            FinalMask = finalMask;

            start = startCheck;
            crcValue = new CRCValue(BitCount, Polynominal, ShiftRight, Initial, FinalMask);
        }

        /// <summary>
        /// 计算数据的校验码，将校验码保存在帧的最后两个字节中。
        /// </summary>
        /// <param name="checkData">被校验的数据帧，要确保校验码以外的字节数据正确完整，函数校验完毕将会把校验码值填入最后两个字节中。</param>
        public override bool GetCheckCode(ref byte[] checkData)
        {
            int len = checkData.Length;
            CountCheckCode(checkData, out checkData[len - 2], out checkData[len - 1]);
            return true;
        }

        /// <summary>
        /// 对数据进行校验。
        /// </summary>
        /// <param name="checkData">被校验的一个完整的帧。</param>
        /// <returns>数据符合校验计算，则返回true，否则返回false.</returns>
        public override bool Check(byte[] checkData)
        {
            byte low, high;
            CountCheckCode(checkData, out high, out low);
            if (low == checkData[checkData.Length - 1] && high == checkData[checkData.Length - 2])
                return true;
            return false;
        }

        /// <summary>
        /// 根据checkData计算校验值赋给高位high和低位low。
        /// </summary>
        /// <param name="checkData">用于校验的源数据</param>
        /// <param name="high">用于接收CRC高位值</param>
        /// <param name="low">用于接收CRC低位值</param>
        protected virtual void CountCheckCode(byte[] checkData, out byte high, out byte low)
        {
            uint ret32bit;
            // 截取被校验的数据
            //             byte[] dd = new byte[checkData.Length - (start + 2)];
            //             Array.Copy(checkData, start, dd, 0, checkData.Length - (start + 2));
            // 
            //             // 计算校验码
            //             ret32bit = crcValue.GetCrc(dd,0, dd.Length);
            ret32bit = crcValue.GetCrc(checkData, 2, checkData.Length - (start + 2));

            // 萃取CRC高、低位校验码
            low = (byte)(0xFF & (byte)ret32bit);
            high = (byte)(0xFF & (byte)(ret32bit >> 8));
        }
        /// <summary>
        /// 根据checkData计算校验值。
        /// </summary>
        /// <param name="checkData">用于校验的源数据</param>
        /// <param name="high">用于接收检验值</param>
        public virtual void CountCheckAllCode(byte[] checkData, out uint ret32bit)
        {
            // 计算校验码
            ret32bit = crcValue.GetCrc(checkData, 0, checkData.Length);
        }
        #endregion
    }

    // A class ===================================================================
    /// <summary>
    /// CRC校验类，获得或检查CRC的校验值。继承自CRCCheck，不同的是此类CRCCheck2是低位在前高位在后。
    /// </summary>
    public class CrcCheckHighBitAtBack : CrcCheck
    {
        /// <summary>
        /// 实例化一个CRC校验对象。注意参数不能错误。
        /// </summary>
        /// <param name="bitCount">位长</param>
        /// <param name="polynominal">多项式</param>
        /// <param name="shiftRight">右移</param>
        /// <param name="initial">初始值</param>
        /// <param name="finalMask">掩码</param>
        public CrcCheckHighBitAtBack(int bitCount, uint polynominal, bool shiftRight, uint initial, uint finalMask)
            : base(bitCount, polynominal, shiftRight, initial, finalMask)
        {
        }
        public CrcCheckHighBitAtBack(int bitCount, uint polynominal, bool shiftRight, uint initial, uint finalMask, int startCheck)
            : base(bitCount, polynominal, shiftRight, initial, finalMask, startCheck)
        {
        }
        public override bool GetCheckCode(ref byte[] checkData)
        {
            int len = checkData.Length;
            CountCheckCode(checkData, out checkData[len - 1], out checkData[len - 2]);
            return true;
        }

        public override bool Check(byte[] checkData)
        {
            byte low, high;
            CountCheckCode(checkData, out high, out low);

            // 把高位和低位颠倒（这里是低位在前，高位在后）
            byte tmp = low;
            low = high;
            high = tmp;

            if (low == checkData[checkData.Length - 2] && high == checkData[checkData.Length - 1])
            {
                return true;
            }
            return false;
        }
    }

    // A class ===================================================================
    /// <summary>
    /// 将校验和前面的部分或全部字节求和,将结果给校验字节。
    /// </summary>
    public class CrcCheckSum : CrcCheck
    {
        /// <summary>
        /// 求和的起始位置
        /// </summary>
        int startPosition = 0;
        /// <summary>
        /// 校验和的长度
        /// </summary>
        int lenOfCheckSum = 1;

        /// <summary>
        /// 实例化一个求和校验服务类。
        /// </summary>
        /// <param name="startPlace">求和的起始位置</param>
        /// <param name="lenOfCheckSum">校验和的长度</param>
        public CrcCheckSum(int startPosition, int lenOfCheckSum)
        {
            this.startPosition = startPosition;
            this.lenOfCheckSum = lenOfCheckSum;
        }

        /// <summary>
        /// 对数据进行校验。
        /// </summary>
        public override bool Check(byte[] checkData)
        {
            byte[] tmpCheckData = (byte[])checkData.Clone();
            GetCheckCode(ref tmpCheckData);

            int checkSumStart = checkData.Length - lenOfCheckSum;
            int checkSumEnd = checkData.Length - 1;
            for (int i = checkSumStart; i <= checkSumEnd; ++i)
            {
                if (checkData[i] != tmpCheckData[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 计算校验和并设置到参数数组中。
        /// </summary>
        public override bool GetCheckCode(ref byte[] checkData)
        {
            int sum = 0;
            int len = checkData.Length - lenOfCheckSum;
            for (int i = startPosition; i < len; ++i)
            {
                sum += checkData[i];
            }

            for (int i = 0; i < lenOfCheckSum; ++i)
            {
                byte tmpSum = (byte)(sum & 0xFF);
                checkData[len + lenOfCheckSum - i - 1] = tmpSum;
                sum = sum >> 8;
            }
            return true;
        }
    }
}