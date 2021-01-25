
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Core.gallery
{
    [EntityName("gallery")]
    public partial class gallery : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _galleryid;
        [DataMember]
        public string galleryId
        {
            get
            {
                return this._galleryid;
            }
            set
            {
                this._galleryid = value;
                SetAttributeValue("galleryId", value);
            }
        }


        /// <summary>
        /// 标签
        /// </summary>
        private string _tags;
        [DataMember]
        public string tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
                SetAttributeValue("tags", value);
            }
        }


        /// <summary>
        /// 预览图
        /// </summary>
        private string _preview_url;
        [DataMember]
        public string preview_url
        {
            get
            {
                return this._preview_url;
            }
            set
            {
                this._preview_url = value;
                SetAttributeValue("preview_url", value);
            }
        }


        /// <summary>
        /// 大图
        /// </summary>
        private string _image_url;
        [DataMember]
        public string image_url
        {
            get
            {
                return this._image_url;
            }
            set
            {
                this._image_url = value;
                SetAttributeValue("image_url", value);
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
        /// 预览图片id
        /// </summary>
        private string _previewid;
        [DataMember]
        public string previewid
        {
            get
            {
                return this._previewid;
            }
            set
            {
                this._previewid = value;
                SetAttributeValue("previewid", value);
            }
        }


        /// <summary>
        /// 大图id
        /// </summary>
        private string _imageid;
        [DataMember]
        public string imageid
        {
            get
            {
                return this._imageid;
            }
            set
            {
                this._imageid = value;
                SetAttributeValue("imageid", value);
            }
        }


    }
}

