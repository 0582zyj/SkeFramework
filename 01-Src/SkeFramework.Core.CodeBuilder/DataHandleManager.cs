using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.CodeBuilder.DataFactory;
using SkeFramework.Core.CodeBuilder.DataHandle;

namespace SkeFramework.Core.CodeBuilder.DAL.Repositorys
{
    public class DataHandleManager
    {
        public static IRepository repository { get { return DbFactory.Instance().CreateRepository(); } }
    }
}
