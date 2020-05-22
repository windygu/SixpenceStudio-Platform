using System;
using System.Collections.Generic;
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
        /// 获取文件列表路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<string> GetFileList(string name)
        {
            var fileList = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "\\bin", name, SearchOption.AllDirectories);
            return fileList;
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
    }
}
