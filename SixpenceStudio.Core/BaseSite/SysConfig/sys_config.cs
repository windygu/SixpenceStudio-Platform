using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysConfig
{
    [EntityName("sys_config")]
    public partial class sys_config : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string sys_configId
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

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        [DataMember]
        public string description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                SetAttributeValue("description", value);
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        private string _value;
        [DataMember]
        public string value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                SetAttributeValue("value", value);
            }
        }
    }
}