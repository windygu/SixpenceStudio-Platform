using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.UserInfo;
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

        public static IEnumerable<sys_role_privilege> GetUserPrivileges(string userId)
        {
            return UserPrivliege.GetOrAdd(UserPrivilegesPrefix + userId, (key) =>
            {
                var broker = PersistBrokerFactory.GetPersistBroker();
                var user = broker.Retrieve<user_info>(userId);
                return broker.RetrieveMultiple<sys_role_privilege>("select * from sys_role_privilege where sys_roleid = @id", new Dictionary<string, object>() { { "@id", user.roleid } }).ToList();
            });
        }
    }
}
