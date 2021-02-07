using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole
{
    public class SysRoleController : EntityBaseController<sys_role, SysRoleService>
    {
        public IEnumerable<SelectOption> GetBasicRole()
        {
            return new SysRoleService().GetBasicRole();
        }
    }
}
