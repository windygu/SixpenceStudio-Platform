using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole.BasicRole
{
    [UnityRegister]
    public interface IBasicRole
    {
        /// <summary>
        /// 获取唯一键
        /// </summary>
        string GetRoleKey { get; }

        /// <summary>
        /// 角色名
        /// </summary>
        SystemRole Role { get; }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        sys_role GetRole();

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<sys_role_privilege> GetRolePrivilege();
    }
}
