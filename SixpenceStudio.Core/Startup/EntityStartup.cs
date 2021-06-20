using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.SysEntity;
using SixpenceStudio.Core.SysEntity.SysAttrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Startup
{
    public class EntityStartup
    {
        public static void Configuration()
        {
#if DEBUG
            var logger = LogFactory.GetLogger("entity");
#endif

            var broker = PersistBrokerFactory.GetPersistBroker();
            var dialect = broker.DbClient.Dialect;
            var entityList = UnityContainerService.ResolveAll<IEntity>();
            broker.ExecuteTransaction(() =>
            {
                // 创建表和初始化数据
                entityList.Each(item =>
                {
                    var entity = broker.Query(dialect.GetTable(item.GetEntityName()));
                    var attrs = item.GetAttrs();
                    if (entity == null || entity.Rows.Count == 0)
                    {
                        var attrSql = attrs
                            .Select(e =>
                            {
                                return $"{e.Name} {e.Type.GetDescription()}{(e.Length != null ? $"({e.Length.Value})" : "")} {(e.IsRequire.HasValue && e.IsRequire.Value ? "NOT NULL" : "")}{(e.Name == $"{item.GetEntityName()}id" ? " PRIMARY KEY" : "")}";
                            })
                            .Aggregate((a, b) => a + ",\r\n" + b);

                        var sql = $@"
CREATE TABLE public.{item.GetEntityName()} (
{attrSql}
)
";
                        // 创建表
                        broker.Execute(sql);
#if DEBUG
                        logger.Info($"实体{item.GetLogicalName()}（{item.GetEntityName()}）创建成功");
#endif

                        // 初始化表数据
                        var initialData = item.GetInitialData().ToList();
                        if (initialData != null && initialData.Count() > 0)
                        {
                            initialData.ForEach(e => broker.Create(e));
#if DEBUG
                            logger.Info($"实体{item.GetLogicalName()}（{item.GetEntityName()}）初始化数据成功");
#endif
                        }
                    }
                });

                // 创建实体记录和实体字段数据
                entityList.Each(item =>
                {
                    #region 实体添加自动写入记录
                    var entity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @code", new Dictionary<string, object>() { { "@code", item.GetEntityName() } });
                    if (entity == null)
                    {
                        entity = new sys_entity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name = item.GetLogicalName(),
                            code = item.GetEntityName(),
                            is_sys = item.IsSystemEntity()
                        };
                        broker.Create(entity);
                    }
                    #endregion

                    #region 字段变更自动写入记录（仅支持新增字段）
                    var attrs = item.GetAttrs();
                    var attrsList = new SysEntityService(broker).GetEntityAttrs(entity.Id).Select(e => e.code);
                    attrs.Each(attr =>
                    {
                        if (!attrsList.Contains(attr.Name))
                        {
                            var _attr = new sys_attrs()
                            {
                                Id = Guid.NewGuid().ToString(),
                                name = attr.LogicalName,
                                code = attr.Name,
                                entityid = entity.Id,
                                entityidname = entity.name,
                                attr_type = attr.Type.GetDescription(),
                                attr_length = attr.Length,
                                isrequire = attr.IsRequire.HasValue && attr.IsRequire.Value
                            };
#if DEBUG
                            logger.Info($"实体{item.GetLogicalName()}（{item.GetEntityName()}）创建字段：{attr.LogicalName}（{attr.Name}）成功");
#endif
                            broker.Create(_attr);
                        }
                    });
                    #endregion
                });
            });
        }
    }
}
