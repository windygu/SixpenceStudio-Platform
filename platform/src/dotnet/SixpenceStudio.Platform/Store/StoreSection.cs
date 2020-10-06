using SixpenceStudio.Platform.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Store
{
    [ConfigSectionName("store")]
    public class StoreSection : ConfigurationSection
    {
        [ConfigurationProperty("temp", DefaultValue = "C:\\temp")]
        public string temp
        {
            get { return (string)this["temp"]; }
            set { this["temp"] = value; }
        }

        [ConfigurationProperty("storage", DefaultValue = "C:\\storage")]
        public string storage
        {
            get { return (string)this["storage"]; }
            set { this["storage"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = "SystemStore")]
        public string type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

    }
}
