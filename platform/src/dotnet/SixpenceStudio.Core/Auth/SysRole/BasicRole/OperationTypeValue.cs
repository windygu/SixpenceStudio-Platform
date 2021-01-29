using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole.BasicRole
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public static class OperationTypeValue
    {
        /// <summary>
        /// 创建
        /// </summary>
        public static SelectOption Create => new SelectOption("创建", "create");

        /// <summary>
        /// 更新
        /// </summary>
        public static SelectOption Update => new SelectOption("更新", "update");

        /// <summary>
        /// 删除
        /// </summary>
        public static SelectOption Delete => new SelectOption("删除", "delete");

        /// <summary>
        /// 查询
        /// </summary>
        public static SelectOption Select => new SelectOption("查询", "select");
    }

    public enum OperationType
    {
        [Description("创建")]
        Create,
        [Description("更新")]
        Update,
        [Description("删除")]
        Delete,
        [Description("查询")]
        Select
    }
}
