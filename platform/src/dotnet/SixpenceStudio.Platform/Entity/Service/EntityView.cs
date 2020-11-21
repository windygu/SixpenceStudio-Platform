﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Entity
{
    /// <summary>
    /// 实体试图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityView<T>
        where T : BaseEntity, new()
    {
        /// <summary>
        /// 视图名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 快速搜索筛选
        /// </summary>
        public IList<string> CustomFilter { get; set; }

        /// <summary>
        /// 视图Id
        /// </summary>
        public string ViewId { get; set; }

        /// <summary>
        /// 视图Sql
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
    }
}