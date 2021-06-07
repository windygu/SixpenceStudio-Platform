using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.ShortUrl
{
    [SystemEntity]
    [EntityName("short_url")]
    public class short_url_log : BaseEntity
    {
        /// <summary>
        /// 短链接key
        /// </summary>
        [DataMember]
        public string short_key
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
        /// 短链接
        /// </summary>
        private string _short_url;
        [DataMember]
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