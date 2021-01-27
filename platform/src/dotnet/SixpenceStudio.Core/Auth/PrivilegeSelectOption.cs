using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth
{
    public static class PrivilegeSelectOption
    {
        [OperationType(OperationTypeValue.CREATE, OperationTypeValue.DELETE, OperationTypeValue.UPDATE, OperationTypeValue.SELECT)]
        public static SelectOption All => new SelectOption("全部", "all");

        [OperationType(OperationTypeValue.CREATE, OperationTypeValue.DELETE, OperationTypeValue.UPDATE, OperationTypeValue.SELECT)]
        public static SelectOption User => new SelectOption("个人", "user");

        [OperationType(OperationTypeValue.CREATE, OperationTypeValue.DELETE, OperationTypeValue.UPDATE, OperationTypeValue.SELECT)]
        public static SelectOption Group => new SelectOption("分组", "group");

        [OperationType(OperationTypeValue.SELECT)]
        public static SelectOption Guest => new SelectOption("游客", "guest");
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public static class OperationTypeValue
    {
        /// <summary>
        /// 创建
        /// </summary>
        public const string CREATE = "create";

        /// <summary>
        /// 创建
        /// </summary>
        public static SelectOption Create => new SelectOption("创建", CREATE);

        /// <summary>
        /// 更新
        /// </summary>
        public const string UPDATE = "update";

        /// <summary>
        /// 更新
        /// </summary>
        public static SelectOption Update => new SelectOption("更新", UPDATE);

        /// <summary>
        /// 删除
        /// </summary>
        public const string DELETE = "delete";

        /// <summary>
        /// 删除
        /// </summary>
        public static SelectOption Delete => new SelectOption("删除", DELETE);

        /// <summary>
        /// 查询
        /// </summary>
        public const string SELECT = "select";

        /// <summary>
        /// 查询
        /// </summary>
        public static SelectOption Select => new SelectOption("查询", SELECT);
    }
}
