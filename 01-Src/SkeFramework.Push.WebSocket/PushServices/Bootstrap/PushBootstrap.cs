using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Bootstrap.Factorys;
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
    public class PushBootstrap: AbstractBootstrap 
    {
        /// <summary>
        /// 数据工厂实例延迟绑定，由应用层进行操作
        /// </summary>
        private static AbstractBootstrap _staticInstance = null;

        public static void SetDataHandleFactory(AbstractBootstrap factory)
        {
            _staticInstance = factory;
        }

        /// <summary>
        /// 推送类型
        /// </summary>
        private PushTypeEumns PushType=PushTypeEumns.None;

        #region 引导程序参数设置
        public AbstractBootstrap SetPushType(PushTypeEumns pushType)
        {
            PushType = pushType;
            return this;
        }
        #endregion

        public override void Validate()
        {
            if (PushType == PushTypeEumns.None)
            {
                throw new ArgumentException("Can't be none", "PushType");
            }
        }


        protected override IPushBroker GetDataHandleCommon<IPushBroker, TData>()
        {
            var dataType = typeof(TData);
            if (IsSubclassOf(typeof(WebSocketNotifications), dataType))
            {
                return new WebSocketPushBroker(BuildPushServerFactory<WebSocketNotifications>(PushType.ToString())) as IPushBroker;
            }
            return base.GetDataHandleCommon<IPushBroker, TData>();
        }

        public override IPushServerFactory<TData> BuildPushServerFactory<TData>(string tableName)
        {
            switch (PushType)
            {
                case PushTypeEumns.WebSocket:
                    return new PushConnectionFactory() as IPushServerFactory<TData>;
            }
            return null;
        }
    }
}
