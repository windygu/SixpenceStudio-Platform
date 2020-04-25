using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity.SysAttrs
{
    public partial class sys_attrs
    {
        [DataMember]
        public string entityCode { get; set; }
    }
}