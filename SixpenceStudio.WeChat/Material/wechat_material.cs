
using SixpenceStudio.Core.Entity;
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
        /// 宽度
        /// </summary>
        private int? _width;
        [DataMember]
        public int? width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                SetAttributeValue("width", value);
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        private int? _height;
        [DataMember]
        public int? height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                SetAttributeValue("height", value);
            }
        }

        /// <summary>
        /// 本地地址
        /// </summary>
        private string _local_url;
        [DataMember]
        public string local_url
        {
            get
            {
                return _local_url;
            }
            set
            {
                _local_url = value;
                SetAttributeValue("local_url", value);
            }
        }

    }
}

