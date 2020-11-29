using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Configs
{
    /// <summary>
    /// 配置工厂类（Web.config）
    /// </summary>
    public static class ConfigFactory
    {
        public const string ConfigFileName = "Web.config";
        private static readonly string ConfigFileFullName;
        static ConfigFactory()
        {
            ConfigFileFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
            if (!File.Exists(ConfigFileFullName))
            {
                throw new SpException("未找到配置文件");
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetConfig<T>(string name = "") where T : ConfigurationSection, new()
        {
            var configMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = ConfigFileFullName
            };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            if (string.IsNullOrEmpty(name))
            {
                var type = typeof(T);
                if (type.IsDefined(typeof(ConfigSectionNameAttribute), true))
                {
                    var nameAttribute = (ConfigSectionNameAttribute)type.GetCustomAttributes(typeof(ConfigSectionNameAttribute), true)[0];
                    name = nameAttribute.Name;
                }
            }
            return (T)config.GetSection(name);
        }
    }
}
