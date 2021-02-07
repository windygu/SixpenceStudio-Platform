
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Core.Auth.SysRolePrivilege
{
    [EntityName("sys_role_privilege")]
    public partial class sys_role_privilege : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string sys_role_privilegeId
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
        /// 角色id
        /// </summary>
        private string _sys_roleid;
        [DataMember]
        public string sys_roleid
        {
            get
            {
                return this._sys_roleid;
            }
            set
            {
                this._sys_roleid = value;
                SetAttributeValue("sys_roleid", value);
            }
        }


        /// <summary>
        /// 角色名
        /// </summary>
        private string _sys_roleidName;
        [DataMember]
        public string sys_roleidName
        {
            get
            {
                return this._sys_roleidName;
            }
            set
            {
                this._sys_roleidName = value;
                SetAttributeValue("sys_roleidName", value);
            }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        private int _privilege;
        [DataMember]
        public int privilege
        {
            get
            {
                return this._privilege;
            }
            set
            {
                this._privilege = value;
                SetAttributeValue("privilege", value);
            }
        }

        /// <summary>
        /// 实体id
        /// </summary>
        private string _sys_entityid;
        [DataMember]
        public string sys_entityid
        {
            get
            {
                return this._sys_entityid;
            }
            set
            {
                this._sys_entityid = value;
                SetAttributeValue("sys_entityid", value);
            }
        }


        /// <summary>
        /// 实体名
        /// </summary>
        private string _sys_entityidName;
        [DataMember]
        public string sys_entityidName
        {
            get
            {
                return this._sys_entityidName;
            }
            set
            {
                this._sys_entityidName = value;
                SetAttributeValue("sys_entityidName", value);
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


    }
}

