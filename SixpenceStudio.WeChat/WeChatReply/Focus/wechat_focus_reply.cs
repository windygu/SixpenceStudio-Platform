using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.WeChatReply.Focus
{
    [EntityName("wechat_focus_reply")]
    public partial class wechat_focus_reply : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string wechat_focus_replyId
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
        private string _content;
        [DataMember]
        public string content
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
        /// 公众号
        /// </summary>
        private string _wechat;
        [DataMember]
        public string wechat
        {
            get
            {
                return this._wechat;
            }
            set
            {
                this._wechat = value;
                SetAttributeValue("wechat", value);
            }
        }


        /// <summary>
        /// 启用
        /// </summary>
        private int _checked;
        [DataMember]
        public int @checked
        {
            get
            {
                return this._checked;
            }
            set
            {
                this._checked = value;
                SetAttributeValue("checked", value);
            }
        }

    }
}
