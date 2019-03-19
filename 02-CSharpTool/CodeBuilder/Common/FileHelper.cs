using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 选择保存文件的名称以及路径  取消返回 空"";
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string SaveFilePathName(string fileName = null, string filter = null, string title = null)
        {
            string path = "";
            System.Windows.Forms.SaveFileDialog fbd = new System.Windows.Forms.SaveFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                fbd.FileName = fileName;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                fbd.Filter = filter;// "Excel|*.xls;*.xlsx;";
            }
            if (!string.IsNullOrEmpty(title))
            {
                fbd.Title = title;// "保存为";
            }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.FileName;
            }
            return path;
        }
        /// <summary>
        /// 选择一个文件
        /// </summary>
        /// <param name="filter">如果需要筛选txt文件（"Files (*.txt)|*.txt"）</param>
        /// <returns></returns>
        private static string SelectFile(string filter = null)
        {
            string path = string.Empty;
            var openFileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Filter = "Files (*.*)|*.*"//如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            };
            if (filter != null)
            {
                openFileDialog.Filter = filter;
            }
            var result = openFileDialog.ShowDialog();
            if (result ==System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }

    }
}
