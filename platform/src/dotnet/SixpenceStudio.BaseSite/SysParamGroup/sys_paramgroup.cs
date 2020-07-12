using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysParamGroup
{
    [EntityName("sys_paramGroup")]
    public partial class sys_paramgroup : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
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