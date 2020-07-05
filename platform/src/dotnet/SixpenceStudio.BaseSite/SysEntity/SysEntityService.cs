using SixpenceStudio.BaseSite.SysEntity.Models;
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

        public override IList<EntityView<sys_entity>> GetViewList()
        {
            var sql = @"
SELECT
	*
FROM
	sys_entity
";
            var customFilter = new List<string>() { "name" };
            return new List<EntityView<sys_entity>>()
            {
                new EntityView<sys_entity>()
                {
                    Sql = sql,
                    CustomFilter = customFilter,
                    OrderBy = "name, createdon",
                    ViewId = "FBEC5163-587B-437E-995F-1DC97229C906",
                    Name = "所有的实体"
                }
            };
        }

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
            var id = "";
            _cmd.broker.ExecuteTransaction(() =>
            {
                _cmd.broker.Execute(DDLTemplate.CreateTable(t.code));
                id = base.CreateData(t);
                var sys_attr = new sys_attrs()
                {
                    sys_attrsId = Guid.NewGuid().ToString(),
                    name = "名称",
                    attr_length = 100,
                    attr_type = "varchar",
                    code = "name",
                    entityCode = t.code,
                    entityid = t.Id,
                    entityidname = t.name,
                    isrequire = 0
                };
                new SysAttrsService(_cmd.broker).CreateData(sys_attr);
            });
            return id;
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
        /// 删除实体
        /// </summary>
        /// <param name="ids"></param>
        public override void DeleteData(List<string> ids)
        {
            _cmd.broker.ExecuteTransaction(() =>
            {
                var dataList = _cmd.broker.RetrieveMultiple<sys_entity>(ids).ToList();
                base.DeleteData(ids); // 删除实体
                var sql = @"
DELETE FROM sys_attrs WHERE entityid IN (in@ids);
";
                _cmd.broker.Execute(sql, new Dictionary<string, object>() { { "in@ids", string.Join(",", ids) } }); // 删除级联字段
                dataList.ForEach(data =>
                {
                    _cmd.broker.Execute(DDLTemplate.DropTable(data.name));
                });
            });
        }
    }

}