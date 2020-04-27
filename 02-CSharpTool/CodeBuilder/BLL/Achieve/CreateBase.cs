using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.BLL.Interfaces;
using CodeBuilder.Common;

namespace CodeBuilder.BLL.Achieve
{
    public abstract class CreateBase : ICreate
    {
        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string filter =string.Empty;

        public string fileName = string.Empty;

        public const string namespace_dll = "SkeFramework";

        public CreateBase(string fileName,string filter = ".cs")
        {
            this.fileName = fileName;
            this.filter = filter;
        }

      
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="TableName">表名</param>
        /// <param name="Content">内容</param>
        /// <param name="Namespace">项目名</param>
        public virtual bool SaveFileString(string Path, string TableName, string Content, string Namespace = "SkeFramework")
        {
            try
            {
                string DealTableName =  ConvertType.ToTitleCase(TableName).Replace("_", "");
                string FilePath = Path + "\\" + Namespace + "\\" + DealTableName + "\\";
                Directory.CreateDirectory(FilePath);
                string FileName = string.Format(fileName, DealTableName);
                File.WriteAllText(FilePath + FileName + filter, Content, Encoding.Unicode);
                return true;
            }
            catch
            {

            }
            return false;
        }

        public abstract string CreateMethod(string TableName, string Namespace = "SkeFramework");


        public virtual bool GenerationCode(string Path, string TableName, string Namespace = "SkeFramework")
        {
            string message = this.CreateMethod(TableName, Namespace);
            return this.SaveFileString(Path, TableName, message, Namespace);
        }
    }
}
