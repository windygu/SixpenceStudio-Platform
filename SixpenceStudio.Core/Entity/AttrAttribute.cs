using System;
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
        /// <param name="type">字段类型</param>
        /// <param name="length">字段长度</param>
        public AttrAttribute(string name, AttrType type, int length)
        {
            this.Name = name;
            this.Type = type;
            this.Length = length;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public AttrType Type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }

      
    }

    /// <summary>
    /// 字段类型枚举
    /// </summary>
    public enum AttrType
    {
        Varchar,
        DateTime,
        Boolean,
        Int,
        Decimal,
        JToken
    }
}
