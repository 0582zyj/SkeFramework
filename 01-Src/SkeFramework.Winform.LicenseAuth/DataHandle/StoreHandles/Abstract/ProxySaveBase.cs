using Newtonsoft.Json.Linq;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles.Abstract
{
    public abstract class ProxySaveBase: SaveBaseHandle
    {
        /// <summary>
        /// 注册码加密方法
        /// </summary>
        private ISecurityHandle security;

        public ProxySaveBase(ISecurityHandle security):base()
        {
            this.security = security;
            this.TextCode = "Code";
        }

        #region 保存加载授权码
        /// <summary>
        /// 获取需要保存的数据
        /// </summary>
        /// <returns>需要存储的信息</returns>
        public override string ToSaveString()
        {
            JObject json = new JObject
            {
                { TextCode, new JValue(FinalCode) }
            };
            if (this.security != null)
            {
                return this.security.Encrypt(json.ToString());
            }
            return json.ToString();
        }
        /// <summary>
        /// 从字符串加载数据
        /// </summary>
        /// <param name="content">字符串数据</param>
        public override void LoadByString(string content)
        {
            FinalCode = "";
            string msg = "";
            if (this.security != null)
            {
                msg =this.security.Decrypt(content);
            }
            JObject json = JObject.Parse(msg);
            if (json.Property(TextCode) != null)
            {
                FinalCode = json.Property(TextCode).Value.Value<string>();
            }
        }
        #endregion
    }
}
