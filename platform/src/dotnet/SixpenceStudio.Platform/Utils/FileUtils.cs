using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SixpenceStudio.Platform.Utils
{
    public static class FileUtils
    {
        public const string FILE_FOLDER = "temp";

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
        public static string GetSystemPath(FolderType type)
        {
            var folderPath = HttpRuntime.AppDomainAppPath;
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
                    folderPath += "\\temp";
                    break;
                default:
                    break;
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
        public static IList<string> GetFileList(string name, FolderType type = FolderType.bin)
        {
            var path = GetSystemPath(type);
            if (!Directory.Exists(path))
            {
                return new List<string>();
            }
            return Directory.GetFiles(path, name, SearchOption.AllDirectories);
        }

        /// <summary>
        /// 获取文件保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetLocalStorage()
        {
            var strpatj = HttpRuntime.AppDomainAppPath;
            if (!Directory.Exists(strpatj + FILE_FOLDER))
                Directory.CreateDirectory(strpatj + FILE_FOLDER);
            return strpatj + FILE_FOLDER;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="image"></param>
        /// <param name="filePath"></param>
        public static void SaveFile(HttpPostedFile image, string filePath)
        {
            // 文件已存在
            if (File.Exists(filePath))
            {
                return;
            }

            try
            {
                image.SaveAs(filePath);
            }
            catch (Exception e)
            {
                throw e;
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

    public enum FolderType
    {
        [Description("dll目录")]
        bin,
        [Description("日志目录")]
        log,
        [Description("日志归档目录")]
        logArchive,
        [Description("临时目录")]
        temp
    }
}
