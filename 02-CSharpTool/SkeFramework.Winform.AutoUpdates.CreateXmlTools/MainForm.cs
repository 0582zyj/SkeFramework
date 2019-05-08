using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SkeFramework.Winform.AutoUpdates.CreateXmlTools
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// 获取当前目录
        /// </summary>
        private string currentDirectory = System.Environment.CurrentDirectory;
        /// <summary>
        /// 服务端xml文件名称
        /// </summary>
        private string serverXmlName = "AutoupdateService.xml";
        /// <summary>
        /// 更新文件URL前缀
        /// </summary>
        private string url { get { return this.txtWebUrl.Text; } }
        /// <summary>
        /// 文件全名
        /// </summary>
        private string FullName { get { return this.url + this.serverXmlName; } }

        public MainForm()
        {
            InitializeComponent();
           
        }

        

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtWebUrl.Text = "http://localhost/Publish/AutoUpgrade/";
            txtWebUrl.ForeColor = Color.Gray;
            this.textDirectory.Text = currentDirectory;
            DirectoryInfo dicInfo = new DirectoryInfo(this.textDirectory.Text);
            if (dicInfo.GetFiles().Length > 0)
            {
                foreach (FileInfo f in dicInfo.GetFiles())
                {
                    if (f.Extension.ToLower().Contains("exe") && !f.Name.Contains("SkeFramework.Winform.AutoUpdates.CreateXmlTools.exe"))
                    {
                        this.textProgramName.Text = f.Name;
                        break;
                    }
                }
            }
        }

        #region 按钮事件
        /// <summary>
        /// 生成XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.CreateXml();
            this.ReadXml();
        }
        /// <summary>
        /// 选择程序目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            string name = FileManager.ChoosePathFileDialogRet();
            if (!String.IsNullOrEmpty(name))
            {
                this.textDirectory.Text = name;
            }
        }
        /// <summary>
        /// 选择程序名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseProgram_Click(object sender, EventArgs e)
        {
            string name = FileManager.OpenFileDialogRet();
            if (!String.IsNullOrEmpty(name))
            {
                this.textProgramName.Text = name;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        /// <summary>
        /// 生成XML
        /// </summary>
        void CreateXml()
        {
            //创建文档对象
            XmlDocument doc = new XmlDocument();
            if (File.Exists(this.FullName))
            {
                doc.LoadXml(this.FullName);
            }
            //创建根节点
            XmlElement root = doc.CreateElement("Config");
            //头声明
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(xmldecl);
            DirectoryInfo dicInfo = new DirectoryInfo(this.textDirectory.Text);
            //添加启动节点
            XmlElement EnabledElement = doc.CreateElement("Enabled");
            EnabledElement.InnerXml = "true";
            root.AppendChild(EnabledElement);
            //添加服务器节点
            XmlElement ServerUrl = doc.CreateElement("ServerUrl");
            ServerUrl.InnerXml = this.FullName;
            root.AppendChild(ServerUrl);
            //添加启动节点
            XmlElement element = doc.CreateElement("ProgramName");
            element.InnerXml = this.textProgramName.Text;
            root.AppendChild(element);

            XmlElement UpdateFileList = doc.CreateElement("UpdateFileList");
            //调用递归方法组装xml文件
            PopuAllDirectory(doc, UpdateFileList, dicInfo);
            root.AppendChild(UpdateFileList);
            //追加节点
            doc.AppendChild(root);
            //保存文档
            doc.Save(serverXmlName);
        }
        /// <summary>
        /// 递归组装xml文件方法
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="root"></param>
        /// <param name="dicInfo"></param>
        private void PopuAllDirectory(XmlDocument doc, XmlElement root, DirectoryInfo dicInfo)
        {
            foreach (FileInfo f in dicInfo.GetFiles())
            {
                //排除当前目录中生成xml文件的工具文件
                if (f.Name != "CreateXmlTools.exe" && f.Name != "AutoupdateService.xml" && !f.FullName.Contains("TempFolder"))
                {
                    string path = dicInfo.FullName.Replace(currentDirectory, "").Replace("\\", "/");
                    string folderPath = string.Empty;
                    if (path != string.Empty)
                    {
                        folderPath = path.TrimStart('/') + "/";
                    }
                    XmlElement child = doc.CreateElement("file");
                    child.SetAttribute("path", folderPath + f.Name);
                    child.SetAttribute("url", url + path + "/" + f.Name);
                    child.SetAttribute("lastver", FileVersionInfo.GetVersionInfo(f.FullName).FileVersion);
                    child.SetAttribute("size", f.Length.ToString());
                    child.SetAttribute("needRestart", "false");
                    child.SetAttribute("version", Guid.NewGuid().ToString());
                    root.AppendChild(child);
                }
            }

            foreach (DirectoryInfo di in dicInfo.GetDirectories())
                PopuAllDirectory(doc, root, di);
        }
        /// <summary>
        /// 读取XML
        /// </summary>
        private void ReadXml()
        {
            string path = "AutoupdateService.xml";
            rtbXml.ReadOnly = true;
            if (File.Exists(path))
            {
                rtbXml.Text = File.ReadAllText(path);
            }
        }

    }
}
