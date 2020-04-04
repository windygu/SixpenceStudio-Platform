using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Configs
{
    public class ConfigInformation
    {
        private static ConfigInformation _configInformation;

        public ConfigInformation Instance
        {
            get
            {
                if (_configInformation == null)
                {
                    _configInformation = new ConfigInformation();
                }
                return _configInformation;
            }
        }

        // 数据库链接字符串加解密 Key Value
        public static string Key = "5D53C9F6-A41E-4A97-8B14-E5D2EA8CC7B2";
        public static string Vector = "334A25EF-780A-44FE-B8B7-8F28E4C67766";
    }
}
