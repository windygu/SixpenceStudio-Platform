using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Startup
{
    public class RoleStartup
    {
        public static void Configuration()
        {
            UnityContainerService
                .ResolveAll<IBasicRole>()
                .Each(item => MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12));
        }
    }
}
