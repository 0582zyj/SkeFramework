using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SkeFramework.Winform.AutoUpdates.Entities.Extends;

namespace SkeFramework.Winform.AutoUpdates
{
    /// <summary>
    /// XML配置类
    /// </summary>
    public class Config
    {

        #region The private fields
        private bool enabled = true;
        private string serverUrl = string.Empty;
        private string programName = string.Empty;
        private UpdateFileList updateFileList = new UpdateFileList();
        #endregion

        #region The public property
        /// <summary>
        /// 是否启用更新
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        /// <summary>
        /// 服务器URL
        /// </summary>
        public string ServerUrl
        {
            get { return serverUrl; }
            set { serverUrl = value; }
        }
        /// <summary>
        /// 程序名称
        /// </summary>
        public string ProgramName
        {
            get { return programName; }
            set { programName = value; }
        }
        /// <summary>
        /// 更新文件列表
        /// </summary>
        public UpdateFileList UpdateFileList
        {
            get { return updateFileList; }
            set { updateFileList = value; }
        }
        #endregion

        #region The public method
        public static Config LoadConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            StreamReader sr = new StreamReader(file);
            Config config = xs.Deserialize(sr) as Config;
            sr.Close();
            return config;
        }

        public void SaveConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            StreamWriter sw = new StreamWriter(file);
            xs.Serialize(sw, this);
            sw.Close();
        }
        #endregion
    }

}
