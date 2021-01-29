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

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public bool CheckAccess(string entityId, OperationType operationType)
        {
            var user = Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUser()?.Id);
            var data = Broker.Retrieve<sys_role_privilege>("select * from sys_role_privilege where roleid = @roleid and entityid = @entityid", new Dictionary<string, object>() { { "@roleid", user.roleid }, { "@entityid", entityId } });
            return (data.privilege & operationType.GetValue<int>()) == operationType.GetValue<int>();
        }
    }
}
