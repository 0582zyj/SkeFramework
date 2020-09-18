using Newtonsoft.Json;
using SkeFramework.Core.WebSocketPush.DataEntities.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.DataCommons
{
    /// <summary>
    /// 通知内容
    /// </summary>
    public class NotificationsVo
    {
        /// <summary>
        /// 通知Code
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 通知内容
        /// </summary>
        public object data { get; set; }

        public NotificationsVo()
        {

        }

        public NotificationsVo(NotificationsType notificationsType,object data)
           : this(data,notificationsType.ToString(), (int)notificationsType, "")
        {

        }

        public NotificationsVo(NotificationsType notificationsType)
            :this(notificationsType.ToString(),(int)notificationsType)
        {

        }
        public NotificationsVo(string type, int code, string msg="")
            :this(null,type,code,msg)
        {

        }
        public NotificationsVo(object data,string type,int code,string msg)
        {
            this.data = data;
            this.type = type;
            this.code = code;
            this.msg = msg;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
