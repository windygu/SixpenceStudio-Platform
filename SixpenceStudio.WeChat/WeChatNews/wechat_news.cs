
using Newtonsoft.Json.Linq;
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.WeChatNews
{
    [EntityName("wechat_news")]
    public partial class wechat_news : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string wechat_newsId
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
        /// 内容
        /// </summary>
        private JToken _content;
        [DataMember]
        public JToken content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                SetAttributeValue("content", value);
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
        /// 上传时间
        /// </summary>
        private long? _update_time;
        [DataMember]
        public long? update_time
        {
            get
            {
                return this._update_time;
            }
            set
            {
                this._update_time = value;
                SetAttributeValue("update_time", value);
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        private string _author;
        [DataMember]
        public string author
        {
            get
            {
                return this._author;
            }
            set
            {
                this._author = value;
                SetAttributeValue("author", value);
            }
        }

        /// <summary>
        /// 封面图片id
        /// </summary>
        private string _thumb_media_id;
        [DataMember]
        public string thumb_media_id
        {
            get
            {
                return this._thumb_media_id;
            }
            set
            {
                this._thumb_media_id = value;
                SetAttributeValue("thumb_media_id", value);
            }
        }

        /// <summary>
        /// 摘要
        /// </summary>
        private string _digest;
        [DataMember]
        public string digest
        {
            get
            {
                return this._digest;
            }
            set
            {
                this._digest = value;
                SetAttributeValue("digest", value);
            }
        }
    }
}

