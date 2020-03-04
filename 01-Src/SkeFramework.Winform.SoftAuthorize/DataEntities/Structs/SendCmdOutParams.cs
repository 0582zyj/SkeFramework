using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataUtils
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SendCmdOutParams
    {
        public uint cBufferSize;
        public DriverStatus DriverStatus;
        public IdSector bBuffer;
    }
}
