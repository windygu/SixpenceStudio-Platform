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
        /// 角色权限id
        /// </summary>
        private string _roleid;
        [DataMember]
        public string roleid
        {
            get
            {
                return _roleid;
            }
            set
            {
                _roleid = value;
                SetAttributeValue("roleid", value);
            }
        }

        /// <summary>
        /// 角色权限名
        /// </summary>
        private string _roleidName;
        [DataMember]
        public string roleidName
        {
            get
            {
                return _roleidName;
            }
            set
            {
                _roleidName = value;
                SetAttributeValue("roleidName", value);
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

        /// <summary>
        /// 锁定
        /// </summary>
        private bool _is_lock;
        [DataMember]
        public bool is_lock
        {
            get
            {
                return this._is_lock;
            }
            set
            {
                this._is_lock = value;
                SetAttributeValue("is_lock", value);
            }
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        private string _is_lockName;
        [DataMember]
        public string is_lockName
        {
            get
            {
                return this._is_lockName;
            }
            set
            {
                this._is_lockName = value;
                SetAttributeValue("is_lockName", value);
            }
        }
    }
}