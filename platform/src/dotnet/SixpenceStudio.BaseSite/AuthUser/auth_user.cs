﻿using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.BaseSite.AuthUser
{
    [EntityName("auth_user")]
    public partial class auth_user : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _auth_userid;
        [DataMember]
        public string auth_userId
        {
            get
            {
                return _auth_userid;
            }
            set
            {
                _auth_userid = value;
                SetAttributeValue("auth_userId", value);
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