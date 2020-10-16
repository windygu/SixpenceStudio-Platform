using SixpenceStudio.Platform.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat
{
    [ConfigSectionName("wechat")]
    public class WeChatSection : ConfigurationSection
    {
        [ConfigurationProperty("token", DefaultValue = "")]
        public string token
        {
            get { return (string)this["token"]; }
            set { this["token"] = value; }
        }

        [ConfigurationProperty("appid", DefaultValue = "")]
        public string appid
        {
            get { return (string)this["appid"]; }
            set { this["appid"] = value; }
        }

        [ConfigurationProperty("secret", DefaultValue = "")]
        public string secret
        {
            get { return (string)this["secret"]; }
            set { this["secret"] = value; }
        }

        [ConfigurationProperty("encodingAESKey", DefaultValue = "")]
        public string encodingAESKey
        {
            get { return (string)this["encodingAESKey"]; }
            set { this["encodingAESKey"] = value; }
        }
    }
}