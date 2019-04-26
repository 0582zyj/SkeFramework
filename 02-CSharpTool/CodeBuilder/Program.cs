using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

namespace CodeBuilder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
       

            //防止程序多开
            var isCreated = false;
            using (var mutex = new Mutex(true, Application.ProductName, out isCreated))
            {
                if (isCreated)
                {
                    RunApplication();
                }
                else
                {
                    XtraMessageBox.Show("程序已经运行，请先关闭。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
    
        }

        static void RunApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            Application.Run(new MainForm());
           
        }
    }
}