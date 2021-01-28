using Microsoft.Win32;
using SkeFramework.Core.NetLog;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SkeFramework.Winform.LicenseAuth.DataUtils
{
    /// <summary>
    /// 注册表工具类
    /// </summary>
    public class RegistryUtil
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="KeyField"></param>
        /// <returns></returns>
        public static string GetRegeditkeyValue(string Path, string KeyField)
        {
            string ret = string.Empty;
            try
            {
                using (var obj = Registry.CurrentUser.OpenSubKey(Path, true))
                {
                    if (obj != null)
                    {
                        var value = obj.GetValue(KeyField);
                        if (value != null)
                            ret = Convert.ToString(value);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
            return ret;
        }
        /// <summary>
        /// 设置键
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="Path"></param>
        /// <param name="KeyField"></param>
        public static void SetSecurityLey(string KeyValue, string Path, string KeyField)
        {
            try
            {
                using (var tempk = Registry.CurrentUser.CreateSubKey(Path))
                {
                    tempk.SetValue(KeyField, KeyValue);
                }
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
        }
    }
}

