
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
        [DataMember]
        public string galleryId
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

