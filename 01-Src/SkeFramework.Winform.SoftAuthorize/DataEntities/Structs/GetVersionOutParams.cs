using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataEntities.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct GetVersionOutParams
    {
        public byte bVersion;
        public byte bRevision;
        public byte bReserved;
        public byte bIDEDeviceMap;
        public uint fCapabilities;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] dwReserved; // For future use.
    }
}
