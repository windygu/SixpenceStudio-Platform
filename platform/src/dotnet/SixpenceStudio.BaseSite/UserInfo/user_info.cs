using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.BaseSite.UserInfo
{
    [EntityName("user_info")]
    public partial class user_info : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        private string _user_infoId;
        [DataMember]
        public string user_infoId
        {
            get
            {
                return _user_infoId;
            }
            set
            {
                _user_infoId = value;
                SetAttributeValue("user_infoId", value);
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
