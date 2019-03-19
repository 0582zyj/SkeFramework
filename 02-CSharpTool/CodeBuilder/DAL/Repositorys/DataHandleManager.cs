using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.DAL.Repository;
using CodeBuilder.DataFactory;

namespace CodeBuilder.DAL.Repositorys
{
    public class DataHandleManager
    {
        public static IRepository repository { get { return DbFactory.Instance().CreateRepository(); } }
    }
}
