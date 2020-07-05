using SixpenceStudio.BaseSite.SysEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity
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
    }
}