using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.WeChat.FocusUser
{
    [EntityName("wechat_user")]
    public partial class wechat_user : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string wechat_userId
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
        /// 是否关注
        /// </summary>
        private int? _subscribe;
        [DataMember]
        public int? subscribe
        {
            get
            {
                return this._subscribe;
            }
            set
            {
                this._subscribe = value;
                SetAttributeValue("subscribe", value);
            }
        }


        /// <summary>
        /// openid
        /// </summary>
        private string _openid;
        [DataMember]
        public string openid
        {
            get
            {
                return this._openid;
            }
            set
            {
                this._openid = value;
                SetAttributeValue("openid", value);
            }
        }


        /// <summary>
        /// 用户的昵称
        /// </summary>
        private string _nickname;
        [DataMember]
        public string nickname
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
                SetAttributeValue("nickname", value);
            }
        }


        /// <summary>
        /// 性别
        /// </summary>
        private int? _sex;
        [DataMember]
        public int? sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
                SetAttributeValue("sex", value);
            }
        }


        /// <summary>
        /// 语言
        /// </summary>
        private string _language;
        [DataMember]
        public string language
        {
            get
            {
                return this._language;
            }
            set
            {
                this._language = value;
                SetAttributeValue("language", value);
            }
        }


        /// <summary>
        /// 城市
        /// </summary>
        private string _city;
        [DataMember]
        public string city
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
                SetAttributeValue("city", value);
            }
        }


        /// <summary>
        /// 省份
        /// </summary>
        private string _province;
        [DataMember]
        public string province
        {
            get
            {
                return this._province;
            }
            set
            {
                this._province = value;
                SetAttributeValue("province", value);
            }
        }


        /// <summary>
        /// 国家
        /// </summary>
        private string _country;
        [DataMember]
        public string country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;
                SetAttributeValue("country", value);
            }
        }


        /// <summary>
        /// 用户画像
        /// </summary>
        private string _headimgurl;
        [DataMember]
        public string headimgurl
        {
            get
            {
                return this._headimgurl;
            }
            set
            {
                this._headimgurl = value;
                SetAttributeValue("headimgurl", value);
            }
        }


        /// <summary>
        /// 关注时间
        /// </summary>
        private DateTime? _subscribe_time;
        [DataMember]
        public DateTime? subscribe_time
        {
            get
            {
                return this._subscribe_time;
            }
            set
            {
                this._subscribe_time = value;
                SetAttributeValue("subscribe_time", value);
            }
        }


        /// <summary>
        /// unionid
        /// </summary>
        private string _unionid;
        [DataMember]
        public string unionid
        {
            get
            {
                return this._unionid;
            }
            set
            {
                this._unionid = value;
                SetAttributeValue("unionid", value);
            }
        }


        /// <summary>
        /// 备注
        /// </summary>
        private string _remark;
        [DataMember]
        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
                SetAttributeValue("remark", value);
            }
        }


        /// <summary>
        /// 分组ID
        /// </summary>
        private int _groupid;
        [DataMember]
        public int groupid
        {
            get
            {
                return this._groupid;
            }
            set
            {
                this._groupid = value;
                SetAttributeValue("groupid", value);
            }
        }


        /// <summary>
        /// 关注渠道
        /// </summary>
        private string _subscribe_scene;
        [DataMember]
        public string subscribe_scene
        {
            get
            {
                return this._subscribe_scene;
            }
            set
            {
                this._subscribe_scene = value;
                SetAttributeValue("subscribe_scene", value);
            }
        }


        /// <summary>
        /// 二维码扫码场景
        /// </summary>
        private int? _qr_scene;
        [DataMember]
        public int? qr_scene
        {
            get
            {
                return this._qr_scene;
            }
            set
            {
                this._qr_scene = value;
                SetAttributeValue("qr_scene", value);
            }
        }


        /// <summary>
        /// 二维码扫码场景描述
        /// </summary>
        private string _qr_scene_str;
        [DataMember]
        public string qr_scene_str
        {
            get
            {
                return this._qr_scene_str;
            }
            set
            {
                this._qr_scene_str = value;
                SetAttributeValue("qr_scene_str", value);
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

