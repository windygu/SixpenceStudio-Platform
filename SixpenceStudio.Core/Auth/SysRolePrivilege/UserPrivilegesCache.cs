using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.UserInfo;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRolePrivilege
{
    public static class UserPrivilegesCache
    {
        private const string UserPrivilegesPrefix = "UserPrivileges";

        private static readonly ConcurrentDictionary<string, IEnumerable<sys_role_privilege>> UserPrivliege = new ConcurrentDictionary<string, IEnumerable<sys_role_privilege>>();

        /// <summary>
        /// 获取用户权限信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<sys_role_privilege> GetUserPrivileges(string userId)
        {
            return UserPrivliege.GetOrAdd(UserPrivilegesPrefix + userId, (key) =>
            {
                var broker = PersistBrokerFactory.GetPersistBroker();
                var user = broker.Retrieve<user_info>(userId);
                return broker.RetrieveMultiple<sys_role_privilege>("select * from sys_role_privilege where sys_roleid = @id", new Dictionary<string, object>() { { "@id", user.roleid } }).ToList();
            });
        }

        /// <summary>
        /// 清除用户权限信息缓存
        /// </summary>
        public static void Clear(IPersistBroker broker)
        {
            UserPrivliege.Clear();
            UnityContainerService.ResolveAll<IBasicRole>().Each(item =>
            {
                (item as BasicRole).Broker = broker;
                item.ClearCache();
                MemoryCacheUtil.RemoveCacheItem(item.GetRoleKey);
                MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12);
            });
        }
    }
}
