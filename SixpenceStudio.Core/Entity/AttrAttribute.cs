using System;
using System.ComponentModel;

namespace SixpenceStudio.Core.Entity
{

    /// <summary>
    /// 字段特性
    /// </summary>
    public sealed class AttrAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="logicalName">字段逻辑名</param>
        /// <param name="type">字段类型</param>
        /// <param name="length">字段长度</param>
        /// <param name="isRequire">是否必填</param>
        public AttrAttribute(string name, string logicalName, AttrType type, int length, bool isRequire = false)
        {
            this.Attr = new Attr()
            {
                Name = name,
                LogicalName = logicalName,
                Type = type,
                Length = length,
                IsRequire = isRequire
            };
        }

        public Attr Attr { get; set; }
    }

    /// <summary>
    /// 字段类型枚举
    /// </summary>
    public enum AttrType
    {
        [Description("varchar")]
        Varchar,
        [Description("timestamp")]
        Timestamp,
        [Description("INT4")]
        Int4,
        [Description("INT8")]
        Int8,
        [Description("numeric")]
        Decimal,
        [Description("jsonb")]
        JToken
    }

    /// <summary>
    /// 字段
    /// </summary>
    public class Attr
    {
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public AttrType Type { get; set; }
        public int Length { get; set; }
        public bool IsRequire { get; set; }
    }
}
