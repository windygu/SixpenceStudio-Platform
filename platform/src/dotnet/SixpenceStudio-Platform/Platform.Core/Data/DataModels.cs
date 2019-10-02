using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Core.Data
{
    public sealed class KeyAttributeLogicalNameAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logicalName"></param>
        public KeyAttributeLogicalNameAttribute(string logicalName)
        {
            LogicalName = logicalName;
        }

        /// <summary>
        /// 数据库表字段名字
        /// </summary>
        public string LogicalName { get; }
    }
}
