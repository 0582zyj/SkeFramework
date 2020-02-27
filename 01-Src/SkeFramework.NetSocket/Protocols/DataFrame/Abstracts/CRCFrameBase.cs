using SkeFramework.NetSocket.Protocols.DataUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Protocols.DataFrame.Abstracts
{
    /// <summary>
    /// Crc帧基类。
    /// <summary>
    public class CRCFrameBase : FrameBase
    {
        [NonSerialized()]
        protected CrcCheckBase crcCheck = null;

        #region CRC
        private CrcCheck g_AppCrc = null;
        public CrcCheck getCrcChecker()
        {
            if (null == g_AppCrc)
                g_AppCrc = new CrcCheck(16, 0x1021, false, 0, 0x00, 2);
            return g_AppCrc;
        }
        /// <summary>
        /// 累加算法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Start"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public long CheckAdd(byte[] data, int Start, int Count)
        {
            if (null == data || (data.Length < (Start + Count)))
            {
                return 0;
            }
            long alladd = 0;
            for (int i = Start; i < Count + Start; i++)
            {
                alladd = alladd + data[i];
            }
            alladd = alladd & 0xFFFFFFFF;
            return alladd;
        }
        public int getDataCrc(byte[] Data, int Start, int Count)
        {
            return (int)CheckAdd(Data, Start, Count);
        }
        #endregion

        public CRCFrameBase() : this(null, null)
        {
        }
        public CRCFrameBase(byte[] data) : this(data, null)
        {
        }

        public CRCFrameBase(byte[] data, byte[] syncHead) : base(data, syncHead)
        {
            crcCheck = new CrcCheck(16, 0x1021, false, 0, 0x00, 4);
        }


        /// <summary>
        /// 设置校验码字节数据。
        /// </summary>
        public virtual void SetCheckBytes()
        {
            if (crcCheck != null)
            {
                crcCheck.GetCheckCode(ref frameBytes);
            }
        }

        /// <summary>
        /// 校验帧数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override ResultOfParsingFrame ParseToFrame(byte[] data)
        {
            if (data.Length == 0) return ResultOfParsingFrame.CrcCheckError;
            //CRC校验
            return base.ParseToFrame(data);
        }

    }
}
