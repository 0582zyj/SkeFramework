﻿using SkeFramework.Winform.LicenseAuth.DataHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPBroadcast
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool result= AuthorizeAgent.Instance().InitAuthorize();
            if (result==false)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
