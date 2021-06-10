using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.ShortUrl
{
    [SystemEntity]
    [EntityName("short_url", "短链接")]
    public class short_url_log : BaseEntity
    {
        [DataMember]
        [Attr("short_url_logid", "实体id",  AttrType.Varchar, 100)]
        public string short_url_logId
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

        private string _short_key;
        /// <summary>
        /// 短链接key
        /// </summary>
        [DataMember]
        [Attr("short_key", "短链接key", AttrType.Varchar, 100)]
        public string short_key
        {
            get
            {
                return this._short_key;
            }
            set
            {
                this._short_key = value;
                SetAttributeValue("short_key", value);
            }
        }

        /// <summary>
        /// 短链接
        /// </summary>
        private string _short_url;
        [DataMember]
        [Attr("short_url", "短链接", AttrType.Varchar, 200)]
        public string short_url
        {
            get
            {
                return this._short_url;
            }
            set
            {
                this._short_url = value;
                SetAttributeValue("short_url", value);
            }
        }

        /// <summary>
        /// 长链接
        /// </summary>
        private string _long_url;
        [DataMember]
        [Attr("long_url", "长链接", AttrType.Text)]
        public string long_url
        {
            get
            {
                return this._long_url;
            }
            set
            {
                this._long_url = value;
                SetAttributeValue("long_url", value);
            }
        }
    }
}