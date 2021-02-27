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
        public const string ROLE_PREFIX = "BasicRole";
        public const string PRIVILEGE_PREFIX = "RolePrivilege";
        public string GetRoleKey => this.GetType().Name;

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
            var roleName = systemRole.GetDescription();
            var key = $"{ROLE_PREFIX}_{systemRole}";
            return MemoryCacheUtil.GetOrAddCacheItem(key, () =>
            {
                var role = broker.Retrieve<sys_role>("select * from sys_role where name = @name", new Dictionary<string, object>() { { "@name", roleName } });
                if (role == null)
                {
                    role = new sys_role()
                    {
                        Id = Guid.NewGuid().ToString(),
                        name = roleName,
                        is_basic = true
                    };
                    broker.Create(role);
                }
                return role;
            }, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        protected virtual IEnumerable<sys_role_privilege> GetRolePrivilege(SystemRole systemRole)
        {
            var roleName = systemRole.ToString();
            var key = $"{PRIVILEGE_PREFIX}_{roleName}";
            return MemoryCacheUtil.GetOrAddCacheItem(key, () =>
            {
                var entityList = GetNoPrivilegeEntityList(systemRole);
                if (!entityList.IsEmpty())
                {
                    CreateRolePrivilege();
                }
                var dataList = broker.RetrieveMultiple<sys_role_privilege>("select * from sys_role_privilege where sys_roleidName = @name", new Dictionary<string, object>() { { "@name", systemRole.GetDescription() } });
                return dataList;
            }, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 创建角色权限
        /// </summary>
        /// <returns></returns>
        protected abstract void CreateRolePrivilege();

        /// <summary>
        /// 生成权限
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="role"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected sys_role_privilege GenerateRolePrivilege(sys_entity entity, sys_role role, int value)
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
                modifiedOn = DateTime.Now,
                privilege = value
            };
            return privilege;
        }

        /// <summary>
        /// 获取角色未生成实体权限的实体
        /// </summary>
        /// <param name="systemRole"></param>
        /// <returns></returns>
        protected IEnumerable<sys_entity> GetNoPrivilegeEntityList(SystemRole systemRole)
        {
            var sql = @"
select *
from sys_entity se 
where sys_entityid not in (
	select sys_entityid 
	from sys_role_privilege srp 
	where sys_roleid  = @roleid
)
";
            return broker.RetrieveMultiple<sys_entity>(sql, new Dictionary<string, object>() { { "@roleid", GetRole(systemRole)?.Id } });
        }
    }
}
