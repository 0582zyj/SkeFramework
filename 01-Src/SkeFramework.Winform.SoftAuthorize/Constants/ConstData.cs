using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.Constants
{
    public class ConstData
    {
        public const uint DFP_GET_VERSION = 0x00074080;
        public const uint DFP_SEND_DRIVE_COMMAND = 0x0007c084;
        public const uint DFP_RECEIVE_DRIVE_DATA = 0x0007c088;

        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint FILE_SHARE_READ = 0x00000001;
        public const uint FILE_SHARE_WRITE = 0x00000002;
        public const uint CREATE_NEW = 1;
        public const uint OPEN_EXISTING = 3;
    }
}
