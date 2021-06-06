using SixpenceStudio.Core.SysEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.SysEntity
{
    /// <summary>
    /// DDL 模板语句
    /// </summary>
    public static class DDLTemplate
    {
        /// <summary>
        /// 返回类型与长度DDL格式语句
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string handleColumnType(string type, int length)
        {
            var longType = new List<string>() { "varchar", "nvarchar", "int" };
            if (longType.Contains(type?.ToLower()))
            {
                return $"{type}({length})";
            }
            return $"{type}";
        }

        /// <summary>
        /// 获取添加字段 Sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string GetAddColumnSql(string tableName, List<Column> columns)
        {
            var sql = $@"
ALTER TABLE {tableName}
";
            var count = 0;
            foreach (var item in columns)
            {
                var itemSql = $"ADD {item.Code} {handleColumnType(item.Type, item.Length)} {(item.IsNotNull ? " NOT NULL" : "")} {(++count == columns.Count ? ";" : ",")}";
                sql += itemSql;
            }
            return sql;
        }

        /// <summary>
        /// 获取删除字段 Sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string GetDropColumnSql(string tableName, List<Column> columns)
        {
            var sql = $@"
ALTER TABLE {tableName}
";
            var count = 0;
            foreach (var item in columns)
            {
                var itemSql = $"DROP COLUMN {item.Code} {(++count == columns.Count ? ";" : ",")}";
                sql += itemSql;
            }
            return sql;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string CreateTable(string tableName)
        {
            var sql = $@"
CREATE TABLE {tableName}
(
{tableName}id VARCHAR(100) PRIMARY KEY
)
";
            return sql;
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string DropTable(string tableName)
        {
            var sql = $@"
DROP TABLE {tableName};
";
            return sql;
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
    }
}