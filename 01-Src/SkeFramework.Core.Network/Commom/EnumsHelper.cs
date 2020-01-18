using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Network.Enums;

namespace SkeFramework.Core.Network.Commom
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumsHelper
    {
        /// <summary>
        /// 获取请求类型枚举值
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
         public static string ValueOfRequestType(RequestTypeEnums enums)  
         {  
             //switch用法  
             switch (enums)  
             {
                 case RequestTypeEnums.GET:
                     return "get";
                 case RequestTypeEnums.POST_FORM:
                case RequestTypeEnums.POST_JSON:
                    return "Post";
                 default:  
                    return "error";
             }  
         }  
    }
}
