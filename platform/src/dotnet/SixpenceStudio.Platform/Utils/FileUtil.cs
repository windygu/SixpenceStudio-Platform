using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Web;

namespace SixpenceStudio.Platform.Utils
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileUtil
    {
        public static string storage = "";
        public static string temp = "";

        static FileUtil()
        {
            var config = ConfigFactory.GetConfig<StoreSection>();
            ExceptionUtil.CheckBoolean<SpException>(config == null, "文件存储配置信息为空", "CA302515-07E6-455C-88C8-5EE130C486D2");
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

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFileType(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var arr = value.Split('.');
                var typeName = arr[arr.Length - 1].ToString();
                return typeName;
            }
            return "";
        }

        /// <summary>
        /// 获取系统路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSystemPath(FolderType type = FolderType.Default)
        {
            var folderPath = string.Empty;
            try
            {
                folderPath = HttpRuntime.AppDomainAppPath;
                switch (type)
                {
                    case FolderType.bin:
                        folderPath += "\\bin";
                        break;
                    case FolderType.log:
                        folderPath += "\\log";
                        break;
                    case FolderType.logArchive:
                        folderPath += "\\log\\Archive";
                        break;
                    case FolderType.temp:
                        folderPath = temp;
                        break;
                    case FolderType.storage:
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

        /// <summary>
        /// 获取文件列表路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<string> GetFileList(string name, FolderType type = FolderType.bin, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var path = GetSystemPath(type);
            if (!Directory.Exists(path))
            {
                return new List<string>();
            }
            return Directory.GetFiles(path, name, searchOption);
        }

        private static byte[] Stream2Byte(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="image"></param>
        /// <param name="filePath"></param>
        public static void SaveFile(Stream stream, string filePath)
        {
            // 文件已存在
            if (File.Exists(filePath))
            {
                return;
            }

            var fs = new FileStream(filePath, FileMode.Create);
            try
            {
                var bytes = Stream2Byte(stream);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
            finally
            {
                fs.Close();
            }

        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="files">文件列表</param>
        /// <param name="targetFolder">目标文件夹</param>
        public static void MoveFiles(List<string> files, string targetFolder)
        {
            files.ForEach(item =>
            {
                FileInfo fileInfo = new FileInfo(item);
                fileInfo.MoveTo(Path.Combine(targetFolder, fileInfo.Name));
            });
        }
    }

    /// <summary>
    /// 目录类型
    /// </summary>
    public enum FolderType
    {
        [Description("默认目录")]
        Default,
        [Description("dll目录")]
        bin,
        [Description("日志目录")]
        log,
        [Description("日志归档目录")]
        logArchive,
        [Description("临时目录")]
        temp,
        [Description("文件存储目录")]
        storage
    }
}
