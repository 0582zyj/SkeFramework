using MicrosServices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.VO
{
    /// <summary>
    /// 菜单
    /// </summary>
   public class MenuSideBar
    {
        public List<PsMenu> root { get; set; }

        public List<PsMenu> child { get; set; }
    }
}
