using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    public class DataModel<T>
        where T : BaseEntity, new()
    {
        public IList<T> DataList { get; set; }
        public int RecordCount { get; set; }
    }

    /// <summary>
    /// 搜索条件
    /// </summary>
    public class SearchCondition
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SearchType Type { get; set; }
    }

    public enum SearchType
    {
        [Description("相等")]
        Equals,
        [Description("相似")]
        Like,
        [Description("大于")]
        Greater,
        [Description("小于")]
        Less,
        [Description("之间")]
        Between,
        [Description("包含")]
        Contains,
        [Description("不包含")]
        NotContains
    }
}
