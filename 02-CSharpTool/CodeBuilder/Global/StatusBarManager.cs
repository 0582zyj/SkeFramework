using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Global
{
    public class StatusBarManager
    {
        public static event Action<string, string> ConnectionStateChanged;
        public static event Action<string> OperationStateChanged;

        private static string _dataBaseName = string.Empty;
        private static string _connectionState = string.Empty;

        private static string _operationState = string.Empty;
        /// <summary>
        /// 当前数据库名
        /// </summary>
        public static string DataBaseName
        {
            get { return _dataBaseName; }
            set
            {
                _dataBaseName = value;
                if (ConnectionStateChanged != null)
                {
                    ConnectionStateChanged(DataBaseName, ConnectionState);
                }
            }
        }
        /// <summary>
        /// 当前数据库状态
        /// </summary>
        public static string ConnectionState
        {
            get { return _connectionState; }
            set
            {
                _connectionState = value;
                if (ConnectionStateChanged != null)
                {
                    ConnectionStateChanged(DataBaseName, ConnectionState);
                }
            }
        }

        public static string OperationState
        {
            get { return _operationState; }
            set
            {
                _operationState = value;
                if (OperationStateChanged != null)
                {
                    OperationStateChanged(OperationState);
                }
            }
        }


    }
}
