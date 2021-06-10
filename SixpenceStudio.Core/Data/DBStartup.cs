using Owin;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Startup;
using SixpenceStudio.Core.SysEntity;
using SixpenceStudio.Core.SysEntity.SysAttrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    public class DBStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var dialect = broker.DbClient.Dialect;
            var entityList = UnityContainerService.ResolveAll<IEntity>();
            broker.ExecuteTransaction(() =>
            {
                entityList.Each(item =>
                {
                    var entity = broker.Query(dialect.GetTable(item.GetEntityName()));
                    var entityid = string.Empty;
                    var attrs = item.GetAttrs();
                    if (entity == null || entity.Rows.Count == 0)
                    {
                        attrs
                            .Select(e =>
                            {
                                return $"{e.Name} {e.Type.GetDescription()}{(e.Length != null ? $"({e.Length.Value})" : "")} {(e.IsRequire.HasValue && e.IsRequire.Value ? "NOT NULL" : "")}{(e.Name == $"{item.GetEntityName()}id" ? " PRIMARY KEY" : "")}";
                            })
                            .Aggregate((a, b) => a + ",\r\n" + b);

                        var sql = $@"
CREATE TABLE public.{item.GetEntityName()} (
{attrs}
)
";
                        broker.Execute(sql);

                        // 创建初始数据
                        var initialData = item.GetInitialData().ToList();
                        if (initialData != null && initialData.Count() > 0)
                        {
                            initialData.ForEach(e => broker.Create(e));
                        }
                        var _entity = new sys_entity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name =item.GetLogicalName(),
                            code = item.GetEntityName(),
                            is_sys = item.IsSystemEntity()
                        };
                        entityid = broker.Create(_entity);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(entityid))
                        {
                            entityid = broker.Retrieve<sys_entity>("select * from sys_entity where code = @code", new Dictionary<string, object>() { { "@code", item.GetEntityName() } })?.Id;
                        }
                        var attrsList = new SysEntityService().GetEntityAttrs(entityid).Select(e => e.code);
                        attrs.Each(attr =>
                        {
                            if (!attrsList.Contains(attr.Name))
                            {
                                var _attr = new sys_attrs()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    name = attr.LogicalName,
                                    code = attr.Name,
                                    entityid = entityid,
                                    entityidname = item.GetEntityName(),
                                    attr_type = attr.Type.GetDescription(),
                                    attr_length = attr.Length,
                                    isrequire = attr.IsRequire.HasValue && attr.IsRequire.Value
                                };
                                broker.Create(_attr);
                            }
                        });
                    }
                });
            });
        }

        public int GetOrderIndex()
        {
            return 50;
        }
    }
}
