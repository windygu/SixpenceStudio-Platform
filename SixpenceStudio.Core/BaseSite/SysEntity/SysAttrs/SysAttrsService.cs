using SixpenceStudio.Core;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SixpenceStudio.Core.Utils;
using SixpenceStudio.Core.Extensions;

namespace SixpenceStudio.Core.SysEntity.SysAttrs
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
            var columns = new List<Attr>()
            {
                { new Attr() { Name = "name", LogicalName = "名称", Type = AttrType.Varchar, Length = 100, IsRequire = false } },
                { new Attr() { Name = "createdBy", LogicalName = "创建人", Type = AttrType.Varchar, Length = 40, IsRequire = true } },
                { new Attr() { Name = "createdByName", LogicalName = "创建人", Type = AttrType.Varchar, Length = 100, IsRequire = true } },
                { new Attr() { Name= "createdOn", LogicalName = "创建日期", Type = AttrType.Timestamp, IsRequire = true } },
                { new Attr() { Name = "modifiedBy", LogicalName = "修改人", Type = AttrType.Varchar, Length = 40, IsRequire = true } },
                { new Attr() { Name = "modifiedByName", LogicalName = "修改人", Type = AttrType.Varchar, Length = 100, IsRequire = true } },
                { new Attr() { Name = "modifiedOn", LogicalName = "修改日期", Type = AttrType.Timestamp, IsRequire = true } }
            };
            _cmd.Broker.ExecuteTransaction(() =>
            {
                var entity = Broker.Retrieve<sys_entity>(id);
                columns.ForEach(item =>
                {
                    var sql = @"
SELECT * FROM sys_attrs
WHERE entityid = @id AND code = @code;
";
                    var count = _cmd.Broker.Query<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", entity.Id }, { "@code", item.Name } }).Count();
                    AssertUtil.CheckBoolean<SpException>(count > 0, $"实体{entity.code}已存在{item.Name}字段，请勿重复添加", "E86150F7-52CC-4FB7-A6C4-B743BF382E92");
                    var attrModel = new sys_attrs()
                    {
                        Id = Guid.NewGuid().ToString(),
                        code = item.Name,
                        name = item.LogicalName,
                        entityid = entity.Id,
                        entityidname = entity.name,
                        attr_type = item.Type.GetDescription(),
                        attr_length = item.Length,
                        isrequire = item.IsRequire == true
                    };
                    _cmd.Create(attrModel);
                });
                Broker.Execute(Broker.DbClient.Dialect.GetAddColumnSql(entity.code, columns));
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
            var columns = new List<Attr>() { { new Attr() {  Name = t?.code, LogicalName = t?.name, Type = t.attr_type.GetEnum<AttrType>(), Length = t.attr_length.Value, IsRequire = t.isrequire } } };
            var sql = Broker.DbClient.Dialect.GetAddColumnSql(t.entityCode, columns);

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
                var columns = new List<Attr>();
                dataList.ForEach(item =>
                {
                    columns.Add(new Attr() { Name = item.code });
                });

                base.DeleteData(ids);

                if (dataList.Count > 0)
                {
                    var tableName = new SysEntityService(_cmd.Broker).GetData(dataList[0].entityid)?.code;
                    var sql = Broker.DbClient.Dialect.GetDropColumnSql(tableName, columns);
                    _cmd.Broker.Execute(sql);
                }
            });
        }
    }
}