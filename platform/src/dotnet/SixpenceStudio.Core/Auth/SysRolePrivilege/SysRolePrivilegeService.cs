using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.UserInfo;
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
    }
}
