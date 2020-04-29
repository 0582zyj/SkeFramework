using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataEntities
{
   public class JwtEntities
    {
        /// <summary>
        /// 发行人
        /// </summary>
        public string iss { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public long exp { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string sub { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string aud { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long iat { get; set; }
        /// <summary>
        /// JWT ID用于标识该JWT
        /// </summary>
        public string jti { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public object data { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
