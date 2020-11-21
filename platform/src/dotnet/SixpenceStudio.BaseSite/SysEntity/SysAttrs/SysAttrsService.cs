using SixpenceStudio.BaseSite.SysEntity.Models;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity.SysAttrs
{
    public class SysAttrsService : EntityService<sys_attrs>
    {
        #region 构造函数
        public SysAttrsService()
        {
            _cmd = new EntityCommand<sys_attrs>();
        }

        public SysAttrsService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_attrs>(broker);
        }
        #endregion

        /// <summary>
        /// 添加系统字段
        /// </summary>
        /// <param name="id"></param>
        public void AddSystemAttrs(string id)
        {
            var columns = new List<Column>()
            {
                { new Column() { Code = "createdBy", Name = "创建人", Type = "varchar", Length = 40, IsNotNull = true } },
                { new Column() { Code = "createdByName", Name = "创建人", Type = "varchar", Length = 100, IsNotNull = true } },
                { new Column() { Code = "createdOn", Name = "创建日期", Type = "timestamp", IsNotNull = true } },
                { new Column() { Code = "modifiedBy", Name = "修改人", Type = "varchar", Length = 40, IsNotNull = true } },
                { new Column() { Code = "modifiedByName", Name = "修改人", Type = "varchar", Length = 100, IsNotNull = true } },
                { new Column() { Code = "modifiedOn", Name = "修改日期", Type = "timestamp", IsNotNull = true } }
            };
            _cmd.Broker.ExecuteTransaction(() =>
            {
                var entity = new SysEntityService(_cmd.Broker).GetData(id);
                columns.ForEach(item =>
                {
                    var sql = @"
SELECT * FROM sys_attrs
WHERE entityid = @id AND code = @code;
";
                    var count = _cmd.Broker.Query<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", entity.Id }, { "@code", item.Code } }).Count();
                    if (count > 0)
                    {
                        throw new SpException("系统字段已存在", "");
                    }
                    var attrModel = new sys_attrs()
                    {
                        Id = Guid.NewGuid().ToString(),
                        code = item.Code,
                        name = item.Name,
                        entityid = entity.Id,
                        entityidname = entity.name,
                        attr_type = item.Type,
                        attr_length = item.Length,
                        isrequire = item.IsNotNull ? 1 : 0
                    };
                    _cmd.Create(attrModel);
                });
                new SysEntityService(_cmd.Broker).AddSystemAttrs(entity.code, columns);
            });
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override string CreateData(sys_attrs t)
        {
            var id = default(string);
            var columns = new List<Column>() { { new Column() { Code = t?.code, Name = t?.name, Type = t?.attr_type, Length = t.attr_length.Value, IsNotNull = t.isrequire.Value == 1 } } };
            var sql = DDLTemplate.GetAddColumnSql(t.entityCode, columns);

            _cmd.Broker.ExecuteTransaction(() =>
            {
                id = base.CreateData(t);
                _cmd.Broker.Execute(sql);
            });

            return id;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ids"></param>
        public override void DeleteData(List<string> ids)
        {
            _cmd.Broker.ExecuteTransaction(() =>
            {
                var dataList = _cmd.Broker.RetrieveMultiple<sys_attrs>(ids).ToList();
                var columns = new List<Column>();
                dataList.ForEach(item =>
                {
                    columns.Add(new Column() { Code = item.code });
                });

                base.DeleteData(ids);

                if (dataList.Count > 0)
                {
                    var tableName = new SysEntityService(_cmd.Broker).GetData(dataList[0].entityid)?.code;
                    var sql = DDLTemplate.GetDropColumnSql(tableName, columns);
                    _cmd.Broker.Execute(sql);
                }
            });
        }
    }
}