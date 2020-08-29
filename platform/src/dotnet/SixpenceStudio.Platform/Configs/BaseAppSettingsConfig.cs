using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace SixpenceStudio.Platform.Configs
{
    /// <summary>
    /// AppSettings 抽象类
    /// </summary>
    public abstract class BaseAppSettingsConfig
    {
        public abstract string Key { get; }

        public string GetValue()
        {
            return ConfigurationManager.AppSettings[Key]?.ToString()?.Trim();
        }

        public void SetValue(string value)
        {
            ConfigurationManager.AppSettings.Set(Key, value);
        }
    }
}
