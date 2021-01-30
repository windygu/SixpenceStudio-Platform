using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole
{
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
