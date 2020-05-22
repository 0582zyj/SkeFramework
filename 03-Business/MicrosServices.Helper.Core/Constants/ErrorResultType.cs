using SkeFramework.Core.Common.ExtendAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Constants
{
     /// <summary>
     /// 错误结果枚举
     /// </summary>
    public enum ErrorResultType
    {
        [EnumAttribute( "菜单名称重复")] ERROR_MENU_NAME_REPEAT = 610,
        [EnumAttribute("父节点不存在")] ERROR_MENU_PARENTNO_NOT_EXISET = 611,
        [EnumAttribute( "菜单编号不存在")] ERROR_MENUNO_NOT_EXISET= 612,
    }
}
