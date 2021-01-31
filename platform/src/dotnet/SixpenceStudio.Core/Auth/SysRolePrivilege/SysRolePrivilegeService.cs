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
        private bool CheckAccess(string entityId, OperationType operationType, string userId)
        {
            var user =Broker.Retrieve<user_info>(string.IsNullOrEmpty(userId) ? UserIdentityUtil.GetCurrentUser()?.Id : userId);
            var data = Broker.Retrieve<sys_role_privilege>(@"
select
	srp.*
from
	sys_role_privilege as srp
where roleid = @role
	and sys_entityid = @entity
", new Dictionary<string, object>() { { "@role", user.roleid }, { "@entity", entityId } });
            return (data.privilege & (int)operationType) == (int)operationType;
        }

        /// <summary>
        /// 检查实体读权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckReadAccess(string entityId, string userId = "")
        {
            return CheckAccess(entityId, OperationType.Read, userId);
        }

        /// <summary>
        /// 检查实体写权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckWriteAccess(string entityId, string userId = "")
        {
            return CheckAccess(entityId, OperationType.Write, userId);
        }

        /// <summary>
        /// 检查实体删权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckDeleteAccess(string entityId, string userId = "")
        {
            return CheckAccess(entityId, OperationType.Delete, userId);
        }
    }
}
