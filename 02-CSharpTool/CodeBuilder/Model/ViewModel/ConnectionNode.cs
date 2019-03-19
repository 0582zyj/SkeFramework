using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Model
{
    public class ConnectionNode
    {
        public string Name { get; set; }

        public string ProviderName { get; set; }

        public string connectionString { get; set; }


        public ConnectionNode(string name, string providerName, string connstr)
        {
            this.Name = name;
            this.ProviderName = providerName;
            this.connectionString = connstr;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
