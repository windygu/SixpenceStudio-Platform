using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole
{
    public static class BasicRoleFactory
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static sys_role GetRole(SystemRole role)
        {
            return MemoryCacheUtil.GetOrAddCacheItem(role.ToString(), () =>
            {
                var broker = PersistBrokerFactory.GetPersistBroker();
                return broker.Retrieve<sys_role>("select * from sys_role where name = @name", new Dictionary<string, object>() { { "@name", role.ToString() } });
            });
        }
    }

    /// <summary>
    /// 基础系统角色
    /// </summary>
    public enum SystemRole
    {
        /// <summary>
        /// 拥有系统所有权限
        /// </summary>
        [Description("系统管理员")]
        Admin,
        /// <summary>
        /// 拥有他人和自己的权限
        /// </summary>
        [Description("高级用户")]
        SuperUser,
        /// <summary>
        /// 拥有自己的权限
        /// </summary>
        [Description("用户")]
        User,
        /// <summary>
        /// 拥有只读权限
        /// </summary>
        [Description("访客")]
        Guest
    }
}
