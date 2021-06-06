using Quartz;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.SysEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.BaseSite.SysEntity
{
    public class EntityRegisterJob : JobBase
    {
        public override string Name => "实体注册";

        public override string Description => "更新注册实体、实体字段信息";

        public override void Executing(IJobExecutionContext context)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var entityList = UnityContainerService.ResolveAll<IEntity>();
            var entityService = new SysEntityService(broker);
            var dataList = entityService.GetAllData();

            broker.ExecuteTransaction(() =>
            {
                entityList.Each(item =>
                {
                    // 没有注册过该实体
                    if (dataList.FirstOrDefault(e => e.EntityName == item.GetEntityName()) == null)
                    {
                        var entity = new sys_entity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name = item.GetEntityName(),
                            code = item.GetEntityName(),
                            is_sys = item.IsSystemEntity(),
                            is_sysName = item.IsSystemEntity() ? "是" : "否"
                        };
                        broker.Create(entity);
                    }
                });
            });
        }
    }
}
