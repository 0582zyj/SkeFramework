using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataUtils
{
    public class HWID
    {
        public static String BIOS { get { return GetWMIIdent("Win32_BIOS", "Manufacturer", "SerialNumber", "SMBIOSBIOSVersion", "IdentificationCode"); } }
        public static String CPU { get { return GetWMIIdent("Win32_Processor", "ProcessorId", "UniqueId", "Name"); } }
        public static String HDD { get { return GetWMIIdent("Win32_DiskDrive", "Model", "TotalHeads"); } }
        public static String GPU { get { return GetWMIIdent("Win32_VideoController", "DriverVersion", "Name"); } }
        public static String MAC { get { return GetWMIIdent("Win32_NetworkAdapterConfiguration", "MACAddress"); } }
        public static String OS { get { return GetWMIIdent("Win32_OperatingSystem", "SerialNumber", "Name"); } }
        public static String SCSI { get { return GetWMIIdent("Win32_SCSIController", "DeviceID", "Name"); } }
        public static String BaseBoard { get { return GetWMIIdent("Win32_BaseBoard", "SerialNumber", "PartNumber"); } }
        public static Boolean IsServer { get { return HDD.Contains("SCSI"); } }

        private static String GetWMIIdent(String Class, String Property)
        {
            var ident = "";
            var objCol = new ManagementClass(Class).GetInstances();
            foreach (var obj in objCol)
            {
                if ((ident = obj.GetPropertyValue(Property) as String) != "")
                    break;
            }
            return ident;
        }

        private static String GetWMIIdent(String Class, params String[] Propertys)
        {
            var ident = "";
            Array.ForEach(Propertys, prop => ident += GetWMIIdent(Class, prop) + " ");
            return ident;
        }
    }

   


}
