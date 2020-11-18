
using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.Material
{
    [EntityName("wechat_material")]
    public partial class wechat_material : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _wechat_materialid;
        [DataMember]
        public string wechat_materialId
        {
            get
            {
                return this._wechat_materialid;
            }
            set
            {
                this._wechat_materialid = value;
                SetAttributeValue("wechat_materialId", value);
            }
        }

        
        /// <summary>
        /// 媒体id
        /// </summary>
        private string _media_id;
        [DataMember]
        public string media_id
        {
            get
            {
                return this._media_id;
            }
            set
            {
                this._media_id = value;
                SetAttributeValue("media_id", value);
            }
        }


        /// <summary>
        /// 地址
        /// </summary>
        private string _url;
        [DataMember]
        public string url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
                SetAttributeValue("url", value);
            }
        }

        /// <summary>
        /// 文件id
        /// </summary>
        private string _sys_fileid;
        [DataMember]
        public string sys_fileid
        {
            get
            {
                return this._sys_fileid;
            }
            set
            {
                this._sys_fileid = value;
                SetAttributeValue("sys_fileid", value);
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        private string _type;
        [DataMember]
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                SetAttributeValue("type", value);
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

