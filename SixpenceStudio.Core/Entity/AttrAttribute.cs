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
        public AttrAttribute(string name, AttrType type, int length, bool isRequire = false)
        {
            this.Attr = new Attr()
            {
                Name = name,
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
        Varchar,
        Timestamp,
        Boolean,
        Int4,
        Decimal,
        JToken
    }

    /// <summary>
    /// 字段
    /// </summary>
    public class Attr
    {
        public string Name { get; set; }
        public AttrType Type { get; set; }
        public int Length { get; set; }
        public bool IsRequire { get; set; }
    }
}
