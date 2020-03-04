using Newtonsoft.Json.Linq;
using SkeFramework.Winform.SoftAuthorize.DataUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.Services.Files
{
    /// <summary>
    /// 文件存储功能的基类，包含了文件存储路径，存储方法等
    /// </summary>
    /// <remarks>
    /// 需要继承才能实现你想存储的数据，比较经典的例子就是存储你的应用程序的配置信息，通常的格式就是xml文件或是json文件。具体请看例子：
    /// </remarks>
    /// <example>
    /// 下面举例实现两个字段的普通数据存储
    /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftFileSaveBaseExample.cs" region="SoftFileSaveBase1" title="简单示例" />
    /// 然后怎么调用呢？
    /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftFileSaveBaseExample.cs" region="Example" title="调用示例" />
    /// 如果你想实现加密存储，这样就不用关心被用户看到了。
    /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftFileSaveBaseExample.cs" region="SoftFileSaveBase2" title="加密示例" />
    /// 如果还是担心被反编译获取数据，那么这个密钥就要来自服务器的数据，本地不做存储。
    /// </example>
    public class SoftFileSaveBase : ISoftFileSaveBase
    {
        #region Constructor

        /// <summary>
        /// 实例化一个文件存储的基类
        /// </summary>
        public SoftFileSaveBase()
        {
            HybirdLock = new SimpleHybirdLock();
        }

        #endregion

        #region Private Member

        private SimpleHybirdLock HybirdLock;                   // 文件存储的同步锁

        #endregion

        #region Protect Member


        /// <summary>
        /// 在日志保存时的标记当前调用类的信息
        /// </summary>
        protected string LogHeaderText { get; set; }

        #endregion

        #region Save Load String


        /// <summary>
        /// 获取需要保存的数据，需要重写实现
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
        /// 从字符串加载数据，需要重写实现
        /// </summary>
        /// <param name="content">字符串数据</param>
        public void LoadByString(string content)
        {
            JObject json = JObject.Parse(content);
            FinalCode= SoftBasic.GetValueFromJsonObject(json, TextCode, FinalCode);
        }

        #endregion

        #region Save Load File

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
                    HybirdLock.Enter();
                    try
                    {
                        using (StreamReader sr = new StreamReader(FileSavePath, Encoding.Default))
                        {
                            LoadByString(decrypt(sr.ReadToEnd()));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        HybirdLock.Leave();
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
                HybirdLock.Enter();
                try
                {
                    using (StreamWriter sw = new StreamWriter(FileSavePath, false, Encoding.Default))
                    {
                        sw.Write(encrypt(ToSaveString()));
                        sw.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    HybirdLock.Leave();
                }
            }
        }


        #endregion

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
    }
}
