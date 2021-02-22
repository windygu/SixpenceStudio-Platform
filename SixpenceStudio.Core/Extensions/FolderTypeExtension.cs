using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SixpenceStudio.Core.Extensions
{
    /// <summary>
    /// 系统文件扩展类
    /// </summary>
    public static class FolderTypeExtension
    {
        public static string storage = "";
        public static string temp = "";

        static FolderTypeExtension()
        {
            var config = ConfigFactory.GetConfig<StoreSection>();
            AssertUtil.CheckBoolean<SpException>(config == null, "文件存储配置信息为空", "CA302515-07E6-455C-88C8-5EE130C486D2");
            temp = config.temp;
            storage = config.storage;
            try
            {
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }
                if (!Directory.Exists(storage))
                {
                    Directory.CreateDirectory(storage);
                }
            }
            catch
            {
                throw new SpException("文件目录初始化失败", "4EC3BE59-CAFB-4FA4-878E-68FFC265487B");
            }
        }

        public static string GetPath(this FolderType type)
        {
            var folderPath = string.Empty;
            try
            {
                folderPath = HttpRuntime.AppDomainAppPath;
                switch (type)
                {
                    case FolderType.Bin:
                        folderPath += "\\bin";
                        break;
                    case FolderType.Log:
                        folderPath += "\\log";
                        break;
                    case FolderType.LogArchive:
                        folderPath += "\\log\\Archive";
                        break;
                    case FolderType.Temp:
                        folderPath = temp;
                        break;
                    case FolderType.Storage:
                        folderPath = storage;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                folderPath = Environment.CurrentDirectory;
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
    }
}
