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
    /// 访客
    /// </summary>
    public class Guest : BasicRole, IBasicRole
    {
        public SystemRole Role { get => SystemRole.Guest; }

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
                var entityList = new EntityCommand<sys_entity>(broker).GetAllEntity().Where(item => !item.is_sys);
                var dataList = entityList.Select(entity =>
                {
                    int privilege = OperationType.Read.GetValue<int>();
                    return GenerateRolePrivilege(entity, GetRole(), privilege);
                }).ToList();
                broker.BulkCreate(dataList);
                return dataList;
            });
        }
    }
}
