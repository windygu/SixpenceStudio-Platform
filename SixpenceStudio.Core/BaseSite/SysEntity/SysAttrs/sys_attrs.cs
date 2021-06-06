using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysEntity.SysAttrs
{
    [KeyAttributes("该实体字段已存在", "entityid", "code")]
    [EntityName("sys_attrs")]
    public partial class sys_attrs : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _sys_attrsid;
        [DataMember]
        public string sys_attrsId
        {
            get
            {
                return this._sys_attrsid;
            }
            set
            {
                this._sys_attrsid = value;
                SetAttributeValue("sys_attrsId", value);
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
                return _code;
            }
            set
            {
                _code = value;
                SetAttributeValue("code", value);
            }
        }

        /// <summary>
        /// 实体id
        /// </summary>
        private string _entityid;
        [DataMember]
        public string entityid
        {
            get
            {
                return _entityid;
            }
            set
            {
                _entityid = value;
                SetAttributeValue("entityid", value);
            }
        }

        /// <summary>
        /// 实体名
        /// </summary>
        private string _entityidname;
        [DataMember]
        public string entityidname
        {
            get
            {
                return _entityidname;
            }
            set
            {
                _entityidname = value;
                SetAttributeValue("entityidname", value);
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        private string _attr_type;
        [DataMember]
        public string attr_type
        {
            get
            {
                return _attr_type;
            }
            set
            {
                _attr_type = value;
                SetAttributeValue("attr_type", value);
            }
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        private int? _attr_length;
        [DataMember]
        public int? attr_length
        {
            get
            {
                return _attr_length;
            }
            set
            {
                _attr_length = value;
                SetAttributeValue("attr_length", value);
            }
        }

        /// <summary>
        /// 是否必填
        /// </summary>
        private bool _isrequire;
        [DataMember]
        public bool isrequire
        {
            get
            {
                return _isrequire;
            }
            set
            {
                _isrequire = value;
                SetAttributeValue("isrequire", value);
            }
        }
    }
}