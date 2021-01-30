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
                        description = roleName,
                        is_basic = true
                    };
                    new SysRoleService(broker).CreateData(role);
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
                var dataList = broker.RetrieveMultiple<sys_role_privilege>("select * from sys_role_privilege where sys_roleidName = @name", new Dictionary<string, object>() { { "@name", systemRole.GetDescription() } });
                if (dataList.IsEmpty())
                {
                    dataList = CreateRolePrivilege();
                }
                return dataList;
            }, DateTime.Now.AddHours(12));
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
        protected sys_role_privilege GenerateRolePrivilege(sys_entity entity, sys_role role, int value)
        {
            var privilege = new sys_role_privilege()
            {
                Id = Guid.NewGuid().ToString(),
                sys_entityid = entity.Id,
                sys_entityidName = entity.name,
                sys_roleid = role.Id,
                sys_roleidName = role.name,
                privilege = value
            };
            return privilege;
        }
    }
}
