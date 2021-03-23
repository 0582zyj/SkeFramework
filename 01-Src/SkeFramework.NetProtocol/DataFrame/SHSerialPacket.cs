using SkeFramework.NetSerialPort.Protocols.DataFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.DataFrame
{
    /// <summary>
    /// 文件传输包
    /// </summary>
    public class SHSerialPacket : PacketFrameBase
    {
        public const int SwitchPacketFrameSize = 32;
        /// <summary>
        /// 数据累计和
        /// </summary>
        private int checkSum;

        public SHSerialPacket(int serialFrameSize) :
            base(serialFrameSize)
        {
        }

        /// <summary>
        /// 累加和
        /// </summary>
        public int CheckSum
        {
            get
            {
                if (checkSum == 0)
                {
                    List<byte> data = this.GetFileTextData();
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            checkSum += (0xFF & item);
                        }
                    }

                }
                return checkSum;
            }
        }
    }
}
