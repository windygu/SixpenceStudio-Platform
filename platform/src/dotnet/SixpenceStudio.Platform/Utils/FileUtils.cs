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
        /// 获取文件保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetLocalStorage()
        {
            var strpatj = HttpRuntime.AppDomainAppPath;
            if (!Directory.Exists(strpatj + "temp"))
                Directory.CreateDirectory(strpatj + "temp");
            return strpatj + "temp";
        }

        public static void SaveFile(string name, string path, Stream stream)
        {

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}
