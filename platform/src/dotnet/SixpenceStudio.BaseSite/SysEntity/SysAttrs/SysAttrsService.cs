using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
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
            var entity = new SysEntityService().GetData(id);
            var columns = new List<Column>()
            {
                { new Column() { Code = "createdby", Name = "创建人", Type = "varchar", Length = 40, IsNotNull = true } },
                { new Column() { Code = "createdbyname", Name = "创建人", Type = "varchar", Length = 100, IsNotNull = true } },
                { new Column() { Code = "createdon", Name = "创建日期", Type = "timestamp", IsNotNull = true } },
                { new Column() { Code = "modifiedby", Name = "修改人", Type = "varchar", Length = 40, IsNotNull = true } },
                { new Column() { Code = "modifiedbyname", Name = "修改人", Type = "varchar", Length = 100, IsNotNull = true } },
                { new Column() { Code = "modifiedon", Name = "修改日期", Type = "timestamp", IsNotNull = true } }
            };
            columns.ForEach(item =>
            {
                var sql = @"
SELECT * FROM sys_attrs
WHERE entityid = @id AND code = @code;
";
                var count = _cmd.broker.Query<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", entity.Id }, { "@code", item.Code } }).Count();
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
            new SysEntityService().AddSystemAttrs(entity.code, columns);
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override string CreateData(sys_attrs t)
        {
            var sql = DDLTemplate.GetAddColumnSql(t.entityCode, new List<Column>() { { new Column() { Code = t.code, Name = t.name, Type = t.attr_type, Length = t.attr_length.Value, IsNotNull = t.isrequire.Value == 1 } } });
            _cmd.broker.DbClient.Execute(sql);
            var id = base.CreateData(t);
            return id;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ids"></param>
        public override void DeletelData(List<string> ids)
        {
            var dataList = _cmd.broker.RetrieveMultiple<sys_attrs>(ids).ToList();
            var columns = new List<Column>();
            dataList.ForEach(item =>
            {
                columns.Add(new Column() { Code = item.code });
            });

            if (dataList.Count > 0)
            {
                var tableName = new SysEntityService().GetData(dataList[0].entityid)?.code;
                var sql = DDLTemplate.GetDropColumnSql(tableName, columns);
                _cmd.broker.DbClient.Execute(sql);
            }

            base.DeletelData(ids);
        }
    }
}