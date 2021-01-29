using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
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
        public string GetRoleKey => PREFIX + Role.ToString();

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
            var entityList = new EntityCommand<sys_entity>(broker).GetAllEntity();
            var dataList = new List<sys_role_privilege>();
            entityList.Each(entity =>
            {
                if (entity.is_sys == 0)
                {
                    dataList.Add(GenerateRolePrivilege(entity, GetRole(), OperationType.Select));
                }
            });
            broker.BulkCreate(dataList);
            return dataList;
        }
    }
}
