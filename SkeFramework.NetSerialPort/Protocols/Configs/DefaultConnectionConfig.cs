using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Protocols.Configs
{
    /// <summary>
    /// 默认连接配置
    /// </summary>
    public class DefaultConnectionConfig : IConnectionConfig
    {
        private readonly Dictionary<string, object> _options = new Dictionary<string, object>();

        public IConnectionConfig SetOption(string optionKey, object optionValue)
        {
            _options[optionKey] = optionValue;
            return this;
        }

        public bool HasOption(string optionKey)
        {
            return _options.ContainsKey(optionKey);
        }

        public bool HasOption<T>(string optionKey)
        {
            return _options.ContainsKey(optionKey) && _options[optionKey] is T;
        }

        public object GetOption(string optionKey)
        {
            if (_options.ContainsKey(optionKey))
            {
                return _options[optionKey];
            }
            return default(object);
        }

        public T GetOption<T>(string optionKey)
        {
            if (_options.ContainsKey(optionKey))
            {
                return (T)_options[optionKey];
            }
            return default(T);
        }


        IList<KeyValuePair<string, object>> IConnectionConfig.Options
        {
            get
            {
                return _options.ToList();
            }
        }
    }
}
