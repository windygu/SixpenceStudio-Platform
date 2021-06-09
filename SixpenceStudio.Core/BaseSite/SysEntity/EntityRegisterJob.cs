using Quartz;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.SysEntity;
using SixpenceStudio.Core.SysEntity.SysAttrs;
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
            var user = UserIdentityUtil.GetCurrentUser();
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
                        var attrs = entity.GetAttrs()
                            .Select(e =>
                            {
                                return new sys_attrs()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    name = e.LogicalName,
                                    entityCode = entity.code,
                                    entityid = entity.Id,
                                    entityidname = entity.name,
                                    attr_length = e.Length,
                                    attr_type = e.Type.GetDescription(),
                                    isrequire = e.IsRequire.HasValue && e.IsRequire.Value,
                                    createdBy = user.Id,
                                    createdByName = user.Name,
                                    createdOn = DateTime.Now,
                                    modifiedBy = user.Id,
                                    modifiedByName = user.Name,
                                    modifiedOn = DateTime.Now
                                };
                            })
                            .ToList();
                        broker.BulkCreate(attrs);
                        broker.Create(entity);
                    }
                });
            });
        }
    }
}
