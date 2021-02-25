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
    /// 高级用户
    /// </summary>
    public class SuperUser : BasicRole, IBasicRole
    {
        public SystemRole Role { get => SystemRole.SuperUser; }

        public sys_role GetRole()
        {
            return GetRole(Role);
        }

        public IEnumerable<sys_role_privilege> GetRolePrivilege()
        {
            return GetRolePrivilege(Role);
        }

        protected override IList<sys_role_privilege> CreateRolePrivilege()
        {
            return broker.ExecuteTransaction(() =>
            {
                var entityList = GetNoPrivilegeEntityList(Role);
                var dataList = entityList.Select(entity =>
                {
                    int privilege = (int)OperationType.Read + (int)OperationType.Write + (int)OperationType.Delete;
                    return GenerateRolePrivilege(entity, GetRole(), privilege);
                }).ToList();
                broker.BulkCreate(dataList);
                return dataList;
            });
        }
    }
}
