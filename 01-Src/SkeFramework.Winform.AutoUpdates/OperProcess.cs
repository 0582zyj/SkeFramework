using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Winform.AutoUpdates.Helpers;

namespace SkeFramework.Winform.AutoUpdates
{
    /// <summary>
    /// 启动进程、关闭进程操作
    /// </summary>
    public class OperProcess
    {
        #region init update env
        public void InitUpdateEnvironment()
        {
            if (IfExist("MainProgram"))
            {
                CloseExe("MainProgram");
            }
        }
        #endregion init update env

        #region updated start process
        public void StartProcess()
        {
            string path = System.Environment.CurrentDirectory;
            if (!IfExist(CommonUnitity.GlobalConfig.ProgramName))
            {
                StartExe(path, CommonUnitity.GlobalConfig.ProgramName);
            }
            CloseExe(CommonUnitity.GlobalConfig.ProgramName);
        }

        #endregion

        #region 启动进程、关闭进程、判断进程是否存在
        //启动exe绝对路径
        private void StartExe(string filePath, string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;//是否使用操作系统外壳程序启动进程

            proc.StartInfo.WorkingDirectory = filePath;//启动进程的初始目录
            proc.StartInfo.FileName = fileName;
            proc.Start();
        }


        //exeName 关闭的exe进程名
        private void CloseExe(string exeName)
        {
            Process[] arrPro = Process.GetProcessesByName(exeName);
            foreach (Process pro in arrPro)
                pro.Kill();
        }
        //processName 进程名
        private bool IfExist(string processName)
        {
            Process[] pro = Process.GetProcessesByName(processName);
            return pro.Count() > 0;
        }
        #endregion 启动进程、关闭进程
    }
}
