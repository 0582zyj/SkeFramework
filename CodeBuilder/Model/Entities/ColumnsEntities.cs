using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Model.Entities
{
   public class ColumnsEntities
    {
       public int ORDINAL_POSITION { get; set; }
       public string COLUMN_NAME { get; set; }
       public string DATA_TYPE { get; set; }
       public string CHARACTER_MAXIMUM_LENGTH { get; set; }
       public string IS_NULLABLE { get; set; }
       public string COLUMN_DEFAULT { get; set; }
       public string COLUMN_COMMENT { get; set; }
    }
}
