using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysParamGroup
{
    [EntityName("sys_paramgroup")]
    public partial class sys_paramgroup : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string sys_paramGroupId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        private string _code;
        [DataMember]
        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
                SetAttributeValue("code", value);
            }
        }
    }
}