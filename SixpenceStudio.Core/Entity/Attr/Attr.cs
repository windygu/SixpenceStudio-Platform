using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    /// <summary>
    /// 字段
    /// </summary>
    public class Attr
    {
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public AttrType Type { get; set; }
        public int? Length { get; set; }
        public bool? IsRequire { get; set; }
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
        JToken,
        [Description("text")]
        Text
    }
}
