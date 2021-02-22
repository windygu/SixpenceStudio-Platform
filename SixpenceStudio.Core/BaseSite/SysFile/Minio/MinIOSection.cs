using SixpenceStudio.Core.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.SysFile.Minio
{
    [ConfigSectionName("minio")]
    public class MinIOSection : ConfigurationSection
    {
        [ConfigurationProperty("endpoint", DefaultValue = "")]
        public string endpoint
        {
            get { return (string)this["endpoint"]; }
            set { this["endpoint"] = value; }
        }

        [ConfigurationProperty("accessKey", DefaultValue = "admin")]
        public string accessKey
        {
            get { return (string)this["accessKey"]; }
            set { this["accessKey"] = value; }
        }

        [ConfigurationProperty("secretKey", DefaultValue = "p@ssw0rd")]
        public string secretKey
        {
            get { return (string)this["secretKey"]; }
            set { this["secretKey"] = value; }
        }

        [ConfigurationProperty("secure", DefaultValue = "false")]
        public string secure
        {
            get { return (string)this["secure"]; }
            set { this["secure"] = value; }
        }

    }
}