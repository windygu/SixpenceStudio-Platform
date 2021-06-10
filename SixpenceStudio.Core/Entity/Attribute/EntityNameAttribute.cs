﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    /// <summary>
    /// 实体名（数据库表名）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EntityNameAttribute : Attribute
    {
        public EntityNameAttribute(string name, string logicalName)
        {
            this.Name = name;
            this.LogicalName = logicalName;
        }
        public string Name { get; set; }
        public string LogicalName { get; set; }
    }
}
