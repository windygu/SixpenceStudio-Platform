using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.SysEntity.Models
{
    public class Column
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public bool IsNotNull { get; set; }
    }
}