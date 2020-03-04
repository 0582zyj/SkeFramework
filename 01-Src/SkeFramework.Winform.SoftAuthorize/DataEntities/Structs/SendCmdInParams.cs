using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataEntities.Structs
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SendCmdInParams
    {
        public uint cBufferSize;
        public IdeRegs irDriveRegs;
        public byte bDriveNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] bReserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] dwReserved;
        public byte bBuffer;
    }
}
