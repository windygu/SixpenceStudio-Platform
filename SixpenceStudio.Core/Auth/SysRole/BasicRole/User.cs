using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
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
    /// <summary>
    /// 普通用户
    /// </summary>
    public class User : BasicRole, IBasicRole
    {
        public SystemRole Role { get => SystemRole.User; }

        public sys_role GetRole()
        {
            return GetRole(Role);
        }

        public IEnumerable<sys_role_privilege> GetRolePrivilege()
        {
            return GetRolePrivilege(Role);
        }

        protected override void CreateRolePrivilege()
        {
            broker.ExecuteTransaction(() =>
            {
                var entityList = GetNoPrivilegeEntityList(Role);
                var dataList = entityList.Select(entity =>
                {
                    int privilege = (int)OperationType.Read + (int)OperationType.Write + (int)OperationType.Delete;
                    return GenerateRolePrivilege(entity, GetRole(), privilege);
                }).ToList();
                broker.BulkCreate(dataList);
            });
        }
    }
}
