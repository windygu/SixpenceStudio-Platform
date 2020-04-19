using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity.SysAttrs
{
    public class sys_attrs : BaseEntity
    {
        #region 构造函数
        public sys_attrs()
        {
            this.EntityName = "sys_attrs";
        }
        #endregion

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
        private int? _isrequire;
        [DataMember]
        public int? isrequire
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

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdby;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdby;
            }
            set
            {
                this._createdby = value;
                SetAttributeValue("CreatedBy", value);
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdbyname;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdbyname;
            }
            set
            {
                this._createdbyname = value;
                SetAttributeValue("CreatedByName", value);
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _createdon;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdon;
            }
            set
            {
                this._createdon = value;
                SetAttributeValue("CreatedOn", value);
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedby;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedby;
            }
            set
            {
                this._modifiedby = value;
                SetAttributeValue("ModifiedBy", value);
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedbyname;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedbyname;
            }
            set
            {
                this._modifiedbyname = value;
                SetAttributeValue("ModifiedByName", value);
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _modifiedon;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedon;
            }
            set
            {
                this._modifiedon = value;
                SetAttributeValue("ModifiedOn", value);
            }
        }
    }
}