using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Bootstrap.Factorys;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.WebSocket.Constants;
using SkeFramework.Push.WebSocket.DataEntities;
using SkeFramework.Push.WebSocket.PushServices.PushFactorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.PushServices.Bootstrap
{
    /// <summary>
    /// 推送引导程序
    /// </summary>
    public class PushBootstrap: AbstractBootstrap 
    {
        /// <summary>
        /// 推送类型
        /// </summary>
        private PushTypeEumns PushType=PushTypeEumns.None;

        public PushBootstrap()
        {
            this.connectionConfig = new DefaultConnectionConfig();
        }

        #region 引导程序参数设置
        /// <summary>
        /// 设置服务端类型
        /// </summary>
        /// <param name="pushType"></param>
        /// <returns></returns>
        public AbstractBootstrap SetPushType(PushTypeEumns pushType)
        {
            PushType = pushType;
            return this;
        }
        #endregion

        #region 服务端和链接设置
        /// <summary>
        /// 检查参数
        /// </summary>
        public override void Validate()
        {
            if (PushType == PushTypeEumns.None)
            {
                throw new ArgumentException("Can't be none", "PushType");
            }
        }
        /// <summary>
        /// 创建服务端具体实现
        /// </summary>
        /// <typeparam name="IPushBroker"></typeparam>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        protected override IPushBroker GetDataHandleCommon<IPushBroker, TNotification>()
        {
            switch (PushType)
            {
                case PushTypeEumns.WebSocket:
                    return new WebSocketPushBroker(BuildPushServerFactory<WebSocketNotifications>()) as IPushBroker;
            }
            return base.GetDataHandleCommon<IPushBroker, TNotification>();
        }
        /// <summary>
        /// 创建服务端链接
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        public override IPushServerFactory<TNotification> BuildPushServerFactory<TNotification>()
        {
            var dataType = typeof(TNotification);
            if (IsSubclassOf(typeof(WebSocketNotifications), dataType))
            {
                return new PushConnectionFactory() as IPushServerFactory<TNotification>;
            }
            return null;
        }
        #endregion

    }
}
