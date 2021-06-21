using SixpenceStudio.Core.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data.DBClient
{
    [ConfigSectionName("db")]
    public class DBSection : ConfigurationSection
    {
        [ConfigurationProperty("sources", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(DBSectionElement), AddItemName = "add")]
        public DBSectionCollection ConfigCollection
        {
            get { return (DBSectionCollection)this["sources"]; }
            set { this["sources"] = value; }
        }
    }

    public class DBSectionCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new DBSectionElement();
        }

        /// <summary>
        /// 获取元素的键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DBSectionElement)element).Name;
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        public IEnumerable<string> AllKeys { get { return BaseGetAllKeys().Cast<string>(); } }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new DBSectionElement this[string name]
        {
            get { return (DBSectionElement)BaseGet(name); }
        }
    }

    public class DBSectionElement : ConfigurationElement
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// 驱动类型
        /// </summary>
        [ConfigurationProperty("driverType", DefaultValue = "Postgresql")]
        public string DriverType
        {
            get { return (string)this["driverType"]; }
            set { this["driverType"] = value; }
        }

        /// <summary>
        /// 值
        /// </summary>
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
