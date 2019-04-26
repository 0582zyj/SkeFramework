using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.Common
{
    public class ConvertType
    {
        /// <summary>
        /// 把数据库的数据类型转换为.NET的数据类型
        /// </summary>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public static string ToNetType(string DataType)
        {
            switch (DataType.ToLower())
            {
                case"integer":
                case "int":
                    return "int";
                case "smallint":
                    return "Int16";
                case "bigint":
                    return "Int64";
                case "nvarchar":
                case "varchar":
                case "char":
                case "nchar":
                case "ntext":
                case "text":
                    return "string";
                case "bit":
                case "boolean":
                    return "bool";
                case "datetime":
                case "smalldatetime":
                case "timestamp":
                    return "DateTime";
                case "tinyint":
                case "blob":
                    return "byte";
                case "money":
                case "smallmoney":
                case "decimal":
                case "numeric":
                    return "decimal";
                case "binary":
                case "image":
                case "varbinary":
                    return "System.Byte[]";
                case "float":
                    return "Double";
                case "real":
                    return "Single";
                case "uniqueidentifier":
                    return "System.Guid";
                case "Variant":
                    return "Object";
                default:
                    return DataType;

            }
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ToTitleCase(string msg)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(msg.ToLower()); 
        }
    }
}
