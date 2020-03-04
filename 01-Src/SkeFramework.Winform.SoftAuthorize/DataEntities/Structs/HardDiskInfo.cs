using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataEntities
{
    [Serializable]
    public struct HardDiskInfo
    {
        /// <summary>
        /// 型号
        /// </summary>
        public string ModuleNumber;
        /// <summary>
        /// 固件版本
        /// </summary>
        public string Firmware;
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber;
        /// <summary>
        /// 容量，以M为单位
        /// </summary>
        public uint Capacity;
    }

}
