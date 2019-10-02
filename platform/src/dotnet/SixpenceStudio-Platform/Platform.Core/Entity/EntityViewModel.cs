using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Platform.Core.Entity
{
    [DataContract]
    public class EntityViewModel
    {
        public EntityViewModel(string id, string name, string sql, ViewType viewType = ViewType.Default)
        {
            Id = id;
            ViewName = name;
            Sql = sql;
            Type = viewType;
        }

        /// <summary>
        /// 视图标识
        /// </summary>
        /// <value>
        /// 标识
        /// </value>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置视图名称
        /// </summary>
        /// <value>
        /// 视图名称
        /// </value>
        [DataMember]
        public string ViewName { get; set; }

        /// <summary>
        /// 获取或设置视图的SQL定义
        /// </summary>
        /// <value>
        /// 视图的SQL定义
        /// </value>
        public string Sql { get; set; }

        /// <summary>
        /// 获取或设置视图的排序定义
        /// </summary>
        /// <value>
        /// 视图的排序定义
        /// </value>
        [DataMember]
        public string OrderBy { get; set; }

        /// <summary>
        /// 获取或设置视图的类型
        /// </summary>
        /// <value>
        /// 视图类型
        /// </value>
        public ViewType Type { get; set; }

        /// <summary>
        /// 获取或设置快速搜索或全局搜索过滤的属性
        /// </summary>
        /// <value>
        /// 快速搜索或全局搜索过滤的属性
        /// </value>
        public List<string> FilterAttrs { get; set; }

        /// <summary>
        /// 获取或设置视图的标签
        /// </summary>
        /// <value>
        /// 视图标签，用于区别同种类型不同的视图
        /// </value>
        public List<string> TagList { get; set; }

        /// <summary>
        /// Model与Entity关联字段的键值对
        /// </summary>
        public Dictionary<string, string> RelatedAttrs { get; set; }

        /// <summary>
        /// 根据模型的字段查询实际实体的字段
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public string GetRelatedAttr(string attr)
        {
            if (RelatedAttrs != null && RelatedAttrs.ContainsKey(attr))
            {
                return RelatedAttrs[attr];
            }

            return null;
        }
    }

    /// <summary>
    /// 视图类型
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// 默认页面视图
        /// </summary>
        Default,

        /// <summary>
        /// 子网格视图
        /// </summary>
        SubGird
    }
}
