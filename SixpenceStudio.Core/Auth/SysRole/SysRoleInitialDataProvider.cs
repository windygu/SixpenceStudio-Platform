using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole
{
    public class SysRoleInitialDataProvider : IEntityInitialDataProvider
    {
        public string EntityName => "sys_role";

        public IEnumerable<BaseEntity> GetInitialData()
        {
            return new List<sys_role>()
            {
                new sys_role() { Id = "00000000-0000-0000-0000-000000000000", is_basic = true, name = "系统管理员", description = "系统管理员", is_sys = true },
                new sys_role() { Id = "111111111-11111-1111-1111-111111111111", name = "访客", description = "访客", is_basic = true, is_sys = true },
                new sys_role() { Id = "222222222-22222-2222-2222-222222222222", name = "用户", description = "用户", is_basic = true, is_sys = true },
                new sys_role() { Id = "333333333-33333-3333-3333-333333333333", name = "高级用户", description = "高级用户", is_basic = true, is_sys = true }
            };
        }
    }
}
