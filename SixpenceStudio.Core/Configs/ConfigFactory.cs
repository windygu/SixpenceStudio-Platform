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
    /// 配置工厂类（Core.config）
    /// </summary>
    public static class ConfigFactory
    {
        public const string ConfigFileName = "Core.config";
        private static readonly string ConfigFileFullName;
        static ConfigFactory()
        {
            var ConfigFileFullNameList = new List<string>()
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", ConfigFileName)
            };
            ConfigFileFullName = ConfigFileFullNameList.FirstOrDefault(item => File.Exists(item));
            if (string.IsNullOrEmpty(ConfigFileFullName))
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
