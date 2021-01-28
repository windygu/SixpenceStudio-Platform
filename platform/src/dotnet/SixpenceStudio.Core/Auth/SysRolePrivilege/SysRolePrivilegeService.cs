using SixpenceStudio.Core.Auth.SysRole;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRolePrivilege
{
    public class SysRolePrivilegeService : EntityService<sys_role_privilege>
    {
        #region 构造函数
        public SysRolePrivilegeService()
        {
            _cmd = new EntityCommand<sys_role_privilege>();
        }

        public SysRolePrivilegeService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_role_privilege>(broker);
        }
        #endregion

        public void CreatePrivilege(sys_role role)
        {
            if (role.is_basic == 1)
            {
                var basicRole = Broker.Retrieve<sys_role>(role.parent_roleid);
            }

        }
    }
}
