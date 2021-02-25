using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.SysEntity
{
    public class SysEntityPlugin : IPersistBrokerPlugin
    {
        public void Execute(PluginContext context)
        {
            if (context.EntityName != "sys_entity") return;

            var obj = context.Entity as sys_entity;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    // 重新注册权限并清除缓存
                    UnityContainerService.ResolveAll<IBasicRole>().Each(item =>
                    {
                        MemoryCacheUtil.RemoveCacheItem(item.GetRoleKey);
                        MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12);
                        UserPrivilegesCache.Clear();
                    });
                    break;
                default:
                    break;
            }
        }
    }
}
