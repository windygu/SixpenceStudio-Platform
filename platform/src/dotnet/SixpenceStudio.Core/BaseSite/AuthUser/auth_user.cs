using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Core.AuthUser
{
    [EntityName("auth_user")]
    [KeyAttributes("用户Id不能重复", "user_infoId")]
    public partial class auth_user : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string auth_userId
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

        private string _password;
        [DataMember]
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                SetAttributeValue("password", value);
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
                SetAttributeValue("createdBy", value);
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
                SetAttributeValue("createdByName", value);
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
                SetAttributeValue("createdOn", value);
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
                SetAttributeValue("modifiedBy", value);
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
                SetAttributeValue("modifiedByName", value);
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
                SetAttributeValue("modifiedOn", value);
            }
        }

        /// <summary>
        /// 用户id
        /// </summary>
        private string _user_infoid;
        [DataMember]
        public string user_infoid
        {
            get
            {
                return _user_infoid;
            }
            set
            {
                _user_infoid = value;
                SetAttributeValue("user_infoid", value);
            }
        }
    }
}