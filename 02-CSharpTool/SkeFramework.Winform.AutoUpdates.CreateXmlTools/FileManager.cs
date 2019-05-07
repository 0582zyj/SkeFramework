using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.AutoUpdates.CreateXmlTools
{
    public class FileManager
    {

        public static String OpenFileDialogRet()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"exe files(*.exe)|*.exe",
                FilterIndex = 1,
                DefaultExt = "exe",
                AddExtension = true,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.SafeFileName;
            }
            return string.Empty;
        }

        public static String ChoosePathFileDialogRet()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            return string.Empty;

        }
    }
}
