using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysParams
{
    [EntityName("sys_param")]
    public partial class sys_param : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string sys_paramId
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

        private string _sys_paramGroupId;
        [DataMember]
        public string sys_paramGroupId
        {
            get
            {
                return this._sys_paramGroupId;
            }
            set
            {
                this._sys_paramGroupId = value;
                SetAttributeValue("sys_paramGroupId", value);
            }
        }

        private string _sys_paramGroupIdName;
        [DataMember]
        public string sys_paramGroupIdName
        {
            get
            {
                return this._sys_paramGroupIdName;
            }
            set
            {
                this._sys_paramGroupIdName = value;
                SetAttributeValue("sys_paramGroupIdName", value);
            }
        }

    }
}