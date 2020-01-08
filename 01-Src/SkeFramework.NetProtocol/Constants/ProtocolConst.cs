using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULCloudLockTool.BLL.SHProtocol.Constants
{
    /// <summary>
    /// 协议任务类型
    /// </summary>
    public class ProtocolConst
    {
        /// <summary>
        /// 6.2.18	上位机广播搜索新主机 (0x12) 
        /// </summary>
        public const int APP_BROADCAST_SEARCH_NEW_HOST = 0x12;
        /// <summary>
        /// AT指令 扫描设备
        /// </summary>
        public const int ATScanDevice = 0x01;
        /// <summary>
        /// 取消扫描
        /// </summary>
        public const int ATCancelScanDevice = 0x02;
        /// <summary>
        /// 设置地址
        /// </summary>
        public const int ATSetRemoteAddress = 0x03;
        /// <summary>
        /// 解析锁信息
        /// </summary>
        public const int ATPraseLockInfo = 0x2B;


        public const int TEST_MODE_SWITCH = 0x10;
        public const int FUNC_MOTOR_CONTROL = 0x11;
        public const int FUNC_WRITE_DATA = 0x12;
        public const int FUNC_READ_DATA = 0x13;
        public const int FUNC_READ_LOCK_DATA = 0x14;
        public const int BLUETOOTH_STATE_READ = 0x15;
        public const int FUNC_WRITE_ELECTLOCK_DATA = 0x16;
        public const int FUNC_READ_ELECTLOCK_DATA = 0x17;
        public const int FUNC_READ_DEVICE_STATE = 0x18;
        public const int FUNC_REPLY_CHECKED_STATE = 0x1A;

        
    }
}
