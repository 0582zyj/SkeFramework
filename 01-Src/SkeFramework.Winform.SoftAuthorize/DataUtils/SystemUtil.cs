using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataUtils
{
    /// <summary>
    /// 生成一个16字节的计算机唯一识别码
    /// 比如: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
    /// </summary>
    public class SystemUtil
    {
        private static string fingerPrint = string.Empty;
        public static string Value()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash("CPU >> " + CpuId() + "\nBIOS >> " +
                                BiosId() + "\nBASE >> " + BaseId()
                            + "\nDISK >> " + DiskId() + "\nVIDEO >> " +
                            VideoId() + "\nMAC >> " + MacId()
                                     );
            }
            return fingerPrint;
        }

        #region Hash
        /// <summary>
        /// 获取Hash
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #endregion

        #region Original Device ID Getting Code
        /// <summary>
        /// 返回CPU
        /// </summary>
        /// <returns></returns>
        private static string CpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = Identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = Identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = Identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = Identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += Identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        /// <summary>
        /// 返回BIOS标识符
        /// </summary>
        /// <returns></returns>
        private static string BiosId()
        {
            return Identifier("Win32_BIOS", "Manufacturer")
            + Identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + Identifier("Win32_BIOS", "IdentificationCode")
            + Identifier("Win32_BIOS", "SerialNumber")
            + Identifier("Win32_BIOS", "ReleaseDate")
            + Identifier("Win32_BIOS", "Version");
        }
        /// <summary>
        /// 主物理硬盘驱动器ID
        /// </summary>
        /// <returns></returns>
        private static string DiskId()
        {
            return Identifier("Win32_DiskDrive", "Model")
            + Identifier("Win32_DiskDrive", "Manufacturer")
            + Identifier("Win32_DiskDrive", "Signature")
            + Identifier("Win32_DiskDrive", "TotalHeads");
        }
        /// <summary>
        /// 主板
        /// </summary>
        /// <returns></returns>
        private static string BaseId()
        {
            return Identifier("Win32_BaseBoard", "Model")
            + Identifier("Win32_BaseBoard", "Manufacturer")
            + Identifier("Win32_BaseBoard", "Name")
            + Identifier("Win32_BaseBoard", "SerialNumber");
        }
        /// <summary>
        /// 显卡
        /// </summary>
        /// <returns></returns>
        private static string VideoId()
        {
            return Identifier("Win32_VideoController", "DriverVersion")
            + Identifier("Win32_VideoController", "Name");
        }
        /// <summary>
        /// 网卡ID
        /// </summary>
        /// <returns></returns>
        private static string MacId()
        {
            return Identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion

        #region BaseMethod
        /// <summary>
        /// 返回硬件标识符
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <param name="wmiMustBeTrue"></param>
        /// <returns></returns>
        private static string Identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            ManagementClass mc =
        new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 返回硬件标识符
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <returns></returns>
        private static string Identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc =
        new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
