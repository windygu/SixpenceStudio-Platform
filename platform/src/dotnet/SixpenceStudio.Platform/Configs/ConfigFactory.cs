using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Configs
{
    public static class ConfigFactory
    {
        public const string ConfigFileName = "Web.config";
        private static readonly string ConfigFileFullName;
        static ConfigFactory()
        {
            var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            ConfigFileFullName = Path.Combine(configFilePath, ConfigFileName);
            if (!File.Exists(ConfigFileFullName))
            {
                throw new SpException("未找到配置文件");
            }
        }

        public static T GetConfig<T>(string name = "") where T : ConfigurationSection
        {
            var configMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = ConfigFileFullName
            };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            return (T)config.GetSection(name);
        }
    }
}
