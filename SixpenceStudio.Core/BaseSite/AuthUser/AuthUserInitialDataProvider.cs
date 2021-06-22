using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.AuthUser
{
    public class AuthUserInitialDataProvider : IEntityInitialDataProvider
    {
        public string EntityName => "auth_user";

        public IEnumerable<BaseEntity> GetInitialData()
        {
            var admin = "00000000-0000-0000-0000-000000000000";
            var Anonymous = "111111111-11111-1111-1111-111111111111";
            return new List<auth_user>()
            {
                new auth_user() { Id = admin, code = "admin", name = "系统管理员", is_lock = false, roleid = admin, roleidName = "系统管理员", password = "4297f44b13955235245b2497399d7a93", user_infoid = admin },
                new auth_user() { Id = Anonymous, code = "anonymous", name = "访客", is_lock = false, roleid = Anonymous, roleidName = "访客", password = "4297f44b13955235245b2497399d7a93", user_infoid = Anonymous },
            };
        }
    }
}
