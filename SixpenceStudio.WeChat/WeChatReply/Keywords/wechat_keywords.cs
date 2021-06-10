
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.WeChatReply.Keywords
{
    [EntityName("wechat_keywords", "微信关键词回复")]
    public partial class wechat_keywords : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string wechat_keywordsId
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
        /// 回复内容
        /// </summary>
        private string _reply_content;
        [DataMember]
        public string reply_content
        {
            get
            {
                return this._reply_content;
            }
            set
            {
                this._reply_content = value;
                SetAttributeValue("reply_content", value);
            }
        }

    }
}

