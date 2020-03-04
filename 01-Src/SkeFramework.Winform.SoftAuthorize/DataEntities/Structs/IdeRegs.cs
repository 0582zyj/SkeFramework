using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataEntities.Structs
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IdeRegs
    {
        public byte bFeaturesReg;
        public byte bSectorCountReg;
        public byte bSectorNumberReg;
        public byte bCylLowReg;
        public byte bCylHighReg;
        public byte bDriveHeadReg;
        public byte bCommandReg;
        public byte bReserved;
    }
}
