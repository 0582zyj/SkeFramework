using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Model
{
   public class DataViewNode
    {
       /// <summary>
       /// 序号
       /// </summary>
       public int ID { get; set; }
       /// <summary>
       /// 名称
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 所属序号
       /// </summary>
       public int ParentID { get; set; }

       public DataViewNode(int Id, string name, int parentid)
       {
           this.ID = Id;
           this.Name = name;
           this.ParentID = parentid;
       }
    }
}
