using SixpenceStudio.Core.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.ShortUrl
{
    [ConfigSectionName("shorturl")]
    public class ShortUrlSection : ConfigurationSection
    {
        [ConfigurationProperty("url", DefaultValue = "http://karldu.cn/#/short/")]
        public string token
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }
    }
}
