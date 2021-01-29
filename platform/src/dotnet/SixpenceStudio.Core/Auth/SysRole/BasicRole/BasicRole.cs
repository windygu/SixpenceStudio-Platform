using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.SysEntity;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole.BasicRole
{
    public abstract class BasicRole
    {
        protected IPersistBroker broker;
        protected const string PREFIX = "BasicRole_";

        public BasicRole()
        {
            broker = PersistBrokerFactory.GetPersistBroker();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        protected virtual sys_role GetRole(SystemRole systemRole)
        {
            var roleName = systemRole.ToString();
            return MemoryCacheUtil.GetOrAddCacheItem(PREFIX + roleName, () =>
            {
                var role = broker.Retrieve<sys_role>("select * from sys_role where name = @name", new Dictionary<string, object>() { { "@name", roleName } });
                if (role == null)
                {
                    var user = UserIdentityUtil.GetAdmin();
                    role = new sys_role()
                    {
                        Id = Guid.NewGuid().ToString(),
                        name = roleName,
                        createdBy = user.Id,
                        createdByName = user.Name,
                        createdOn = DateTime.Now,
                        modifiedBy = user.Id,
                        modifiedByName = user.Name,
                        modifiedOn = DateTime.Now,
                        description = systemRole.GetDescription(),
                        is_basic = 1
                    };
                    new SysRoleService(broker).CreateData(role);
                }
                return role;
            });
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        protected virtual IEnumerable<sys_role_privilege> GetRolePrivilege(SystemRole systemRole)
        {
            var roleName = systemRole.ToString();
            return MemoryCacheUtil.GetOrAddCacheItem(PREFIX + roleName, () =>
            {
                var dataList = broker.RetrieveMultiple<sys_role_privilege>("select * from sys_role_privilege where sys_roleidName = @name", new Dictionary<string, object>() { { "@name", roleName } });
                if (dataList.IsEmpty())
                {
                    dataList = CreateRolePrivilege();
                }
                return dataList;
            });
        }

        /// <summary>
        /// 创建角色权限
        /// </summary>
        /// <returns></returns>
        protected abstract IList<sys_role_privilege> CreateRolePrivilege();

        /// <summary>
        /// 生成权限
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="role"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected sys_role_privilege GenerateRolePrivilege(sys_entity entity, sys_role role, OperationType type)
        {
            var user = UserIdentityUtil.GetAdmin();
            var privilege = new sys_role_privilege()
            {
                Id = Guid.NewGuid().ToString(),
                sys_entityid = entity.Id,
                sys_entityidName = entity.name,
                sys_roleid = role.Id,
                sys_roleidName = role.name,
                createdBy = user.Id,
                createdByName = user.Name,
                createdOn = DateTime.Now,
                modifiedBy = user.Id,
                modifiedByName = user.Name,
                modifiedOn = DateTime.Now
            };
            switch (type)
            {
                case OperationType.Create:
                    privilege.operation_type = OperationTypeValue.Create.Value;
                    privilege.operation_typeName = OperationTypeValue.Create.Name;
                    break;
                case OperationType.Delete:
                    privilege.operation_type = OperationTypeValue.Delete.Value;
                    privilege.operation_typeName = OperationTypeValue.Delete.Name;
                    break;
                case OperationType.Select:
                    privilege.operation_type = OperationTypeValue.Select.Value;
                    privilege.operation_typeName = OperationTypeValue.Select.Name;
                    break;
                case OperationType.Update:
                    privilege.operation_type = OperationTypeValue.Update.Value;
                    privilege.operation_typeName = OperationTypeValue.Update.Name;
                    break;
                default:
                    break;
            }
            return privilege;
        }
    }
}
