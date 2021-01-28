using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Core.Auth.SysRole
{
    [EntityName("sys_role")]
    public partial class sys_role : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _sys_roleid;
        [DataMember]
        public string sys_roleId
        {
            get
            {
                return this._sys_roleid;
            }
            set
            {
                this._sys_roleid = value;
                SetAttributeValue("sys_roleId", value);
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
        /// 是否基础角色
        /// </summary>
        private int _is_basic;
        [DataMember]
        public int is_basic
        {
            get
            {
                return this._is_basic;
            }
            set
            {
                this._is_basic = value;
                SetAttributeValue("is_basic", value);
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdBy;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
                SetAttributeValue("createdBy", value);
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdByName;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdByName;
            }
            set
            {
                this._createdByName = value;
                SetAttributeValue("createdByName", value);
            }
        }


        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _createdOn;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdOn;
            }
            set
            {
                this._createdOn = value;
                SetAttributeValue("createdOn", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedBy;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedBy;
            }
            set
            {
                this._modifiedBy = value;
                SetAttributeValue("modifiedBy", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedByName;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedByName;
            }
            set
            {
                this._modifiedByName = value;
                SetAttributeValue("modifiedByName", value);
            }
        }


        /// <summary>
        /// 修改日期
        /// </summary>
        private DateTime? _modifiedOn;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedOn;
            }
            set
            {
                this._modifiedOn = value;
                SetAttributeValue("modifiedOn", value);
            }
        }


        /// <summary>
        /// 继承角色
        /// </summary>
        private string _parent_roleid;
        [DataMember]
        public string parent_roleid
        {
            get
            {
                return this._parent_roleid;
            }
            set
            {
                this._parent_roleid = value;
                SetAttributeValue("parent_roleid", value);
            }
        }


        /// <summary>
        /// 继承角色
        /// </summary>
        private string _parent_roleidName;
        [DataMember]
        public string parent_roleidName
        {
            get
            {
                return this._parent_roleidName;
            }
            set
            {
                this._parent_roleidName = value;
                SetAttributeValue("parent_roleidName", value);
            }
        }
    }
}

