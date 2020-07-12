using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Entity
{
    public class DataModel<T>
        where T : BaseEntity, new()
    {
        public IList<T> DataList { get; set; }
        public int RecordCount { get; set; }
    }

    /// <summary>
    /// 选项集
    /// </summary>
    public class SelectModel
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    /// <summary>
    /// 搜索条件
    /// </summary>
    public class SearchCondition
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
