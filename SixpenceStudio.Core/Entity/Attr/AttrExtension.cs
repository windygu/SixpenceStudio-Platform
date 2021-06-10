using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    public static class AttrExtension
    {
        /// <summary>
        /// 字段类型转 AttrType 枚举
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToAttrString(this string value)
        {
            switch (value)
            {
                case "varchar":
                case "text":
                    return "Varchar";
                case "timestamp":
                    return "Timestamp";
                case "INT4":
                    return "Int4";
                case "INT8":
                    return "Int8";
                case "json":
                    return "JToken";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 转换成C#类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToCSharpType(this string type)
        {
            switch (type)
            {
                case "varchar":
                case "text":
                    return "string";
                case "timestamp":
                    return "DateTime?";
                case "INT4":
                    return "int?";
                case "json":
                    return "JToken";
                default:
                    return "";
            }
        }
    }
}
