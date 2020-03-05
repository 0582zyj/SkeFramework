using Newtonsoft.Json.Linq;
using SkeFramework.Winform.SoftAuthorize.DataUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataHandle.FilesHandles
{
    /// <summary>
    /// 文件存储功能的基类，包含了文件存储路径，存储方法等
    /// </summary>
    /// <remarks>
    public class FilesSaveHandles : ISaveHandles
    {
  

        #region 公共属性

        /// <summary>
        /// 文件存储的路径
        /// </summary>
        public string FileSavePath { get; set; }
        /// <summary>
        /// 注册描述字符
        /// </summary>
        public string TextCode { get; set; }
        /// <summary>
        /// 注册码
        /// </summary>
        public string FinalCode { get; set; }
        #endregion

        #region 构造函数

        /// <summary>
        /// 实例化一个文件存储的基类
        /// </summary>
        public FilesSaveHandles()
        {
        }

        #endregion

        #region 保存加载授权码
        /// <summary>
        /// 获取需要保存的数据
        /// </summary>
        /// <returns>需要存储的信息</returns>
        public string ToSaveString()
        {
            JObject json = new JObject
            {
                { TextCode, new JValue(FinalCode) }
            };
            return  json.ToString();
        }
        /// <summary>
        /// 从字符串加载数据
        /// </summary>
        /// <param name="content">字符串数据</param>
        public void LoadByString(string content)
        {
            JObject json = JObject.Parse(content);
            if (json.Property(TextCode) != null)
            {
                FinalCode = json.Property(TextCode).Value.Value<string>();
            }
            else
            {
                FinalCode = "";
            }
        }
        #endregion

        #region 保存加载文件

        /// <summary>
        /// 使用用户自定义的解密方法从文件读取数据
        /// </summary>
        /// <param name="decrypt">用户自定义的解密方法</param>
        public void LoadByFile(Converter<string, string> decrypt)
        {
            if (FileSavePath != "")
            {
                if (File.Exists(FileSavePath))
                {
                    using (StreamReader sr = new StreamReader(FileSavePath, Encoding.Default))
                    {
                        LoadByString(decrypt(sr.ReadToEnd()));
                    }
                }
            }
        }
        /// <summary>
        /// 使用用户自定义的加密方法保存数据到文件
        /// </summary>
        /// <param name="encrypt">用户自定义的加密方法</param>
        public void SaveToFile(Converter<string, string> encrypt)
        {
            if (FileSavePath != "")
            {
                using (StreamWriter sw = new StreamWriter(FileSavePath, false, Encoding.Default))
                {
                    sw.Write(encrypt(ToSaveString()));
                    sw.Flush();
                }
            }
        }

    
        #endregion

    }
}
