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

    /// <summary>
    /// 主键自定义属性
    /// TODO：使用 Plugin 方式做主键唯一性检查
    /// </summary>
    public sealed class KeyAttributesAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public KeyAttributesAttribute(string repeatMessage, params string[] attributes)
        {
            RepeatMessage = repeatMessage;
            AttributeList = new List<string>(attributes);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string RepeatMessage { get; }

        /// <summary>
        /// 字段
        /// </summary>
        public IList<string> AttributeList { get; }
    }
}
