using Owin;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Startup;
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
                    if (entity == null || entity.Rows.Count == 0)
                    {
                        var attrs = item.GetAttrs()
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
