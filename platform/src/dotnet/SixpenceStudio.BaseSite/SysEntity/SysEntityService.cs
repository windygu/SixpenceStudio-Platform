using SixpenceStudio.BaseSite.SysEntity.SysAttrs;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity
{
    public class SysEntityService : EntityService<sys_entity>
    {
        #region 构造函数
        public SysEntityService()
        {
            _cmd = new EntityCommand<sys_entity>();
        }
        
        public SysEntityService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_entity>(broker);
        }
        #endregion

        /// <summary>
        /// 根据实体 id 查询字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<sys_attrs> GetEntityAttrs(string id)
        {
            var sql = @"
SELECT
	*
FROM 
	sys_attrs
WHERE
	entityid = @id
";
            return _cmd.broker.RetrieveMultiple<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", id } });
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override string CreateData(sys_entity t)
        {
            CreateTable(t.code);
            return base.CreateData(t);
        }

        /// <summary>
        /// 实体添加系统字段
        /// </summary>
        /// <param name="tableName"></param>
        public void AddSystemAttrs(string tableName, List<Column> columns)
        {
            var sql = DDLTemplate.GetAddColumnSql(tableName, columns);
            _cmd.broker.Execute(sql);
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        public void CreateTable(string tableName)
        {
            _cmd.broker.Execute(DDLTemplate.CreateTable(tableName));
        }
    }

    #region DDL语句模板
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
    }
    public class Column
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public bool IsNotNull { get; set; }
    }
    #endregion

}