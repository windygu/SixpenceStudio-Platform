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

            var broker = context.Broker;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    // 重新注册权限并清除缓存
                    UserPrivilegesCache.Clear(broker);
                    break;
                case EntityAction.PostDelete:
                    var privileges = new SysRolePrivilegeService(broker).GetPrivileges(context.Entity.Id).ToArray();
                    broker.Delete(privileges);
                    UserPrivilegesCache.Clear(broker);
                    break;
                default:
                    break;
            }
        }
    }
}
