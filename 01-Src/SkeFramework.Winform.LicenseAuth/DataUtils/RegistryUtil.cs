using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataUtils
{
    class RegistryUtil
    {
        const string _uriDeviecId = "SOFTWARE\\YourCompany\\YouApp";
        public static string GetDeviceId()
        {
            string ret = string.Empty;
            using (var obj = Registry.LocalMachine.OpenSubKey(_uriDeviecId, false))
            {
                if (obj != null)
                {
                    var value = obj.GetValue("DeviceId");
                    if (value != null)
                        ret = Convert.ToString(value);
                }
            }
            return ret;
        }

        public static void SetDeviceId()
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                string id = sBuilder.ToString();
                using (var tempk = Registry.LocalMachine.CreateSubKey(_uriDeviecId))
                {
                    tempk.SetValue("DeviceId", id);
                }
            }
        }
    }
}
