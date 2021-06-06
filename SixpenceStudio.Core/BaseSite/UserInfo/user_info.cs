using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Core.UserInfo
{
    [EntityName("user_info")]
    public partial class user_info : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public string user_infoId
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
                return _code;
            }
            set
            {
                _code = value;
                SetAttributeValue("code", value);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private int? _gender;
        [DataMember]
        public int? gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                SetAttributeValue("gender", value);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private string _genderName;
        [DataMember]
        public string genderName
        {
            get
            {
                return _genderName;
            }
            set
            {
                _genderName = value;
                SetAttributeValue("genderName", value);
            }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        private string _realname;
        [DataMember]
        public string realname
        {
            get
            {
                return _realname;
            }
            set
            {
                _realname = value;
                SetAttributeValue("realname", value);
            }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        private string _mailbox;
        [DataMember]
        public string mailbox
        {
            get
            {
                return _mailbox;
            }
            set
            {
                _mailbox = value;
                SetAttributeValue("mailbox", value);
            }
        }

        /// <summary>
        /// 个人介绍
        /// </summary>
        private string _introduction;
        [DataMember]
        public string introduction
        {
            get
            {
                return _introduction;
            }
            set
            {
                _introduction = value;
                SetAttributeValue("introduction", value);
            }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        private string _cellphone;
        [DataMember]
        public string cellphone
        {
            get
            {
                return _cellphone;
            }
            set
            {
                _cellphone = value;
                SetAttributeValue("cellphone", value);
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        private string _avatar;
        [DataMember]
        public string avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
                SetAttributeValue("avatar", value);
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

        private int? _stateCode;
        [DataMember]
        public int? stateCode
        {
            get
            {
                return this._stateCode;
            }
            set
            {
                this._stateCode = value;
                SetAttributeValue("stateCode", value);
            }
        }

        private string _stateCodeName;
        [DataMember]
        public string stateCodeName
        {
            get
            {
                return this._stateCodeName;
            }
            set
            {
                this._stateCodeName = value;
                SetAttributeValue("stateCodeName", value);
            }
        }
    }
}
