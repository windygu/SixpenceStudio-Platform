using SixpenceStudio.BaseSite.DataService.Models;
using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SixpenceStudio.Platform.Utils;

namespace SixpenceStudio.BaseSite.DataService
{
    public class DataService
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public ImageInfo UploadImage(HttpPostedFile image, string fileType)
        {
            // 获取文件哈希码，将哈希码作为文件名
            var hash_code = SHAUtils.GetFileSHA1(image.InputStream);
            var id = Guid.NewGuid().ToString();
            var fileName = $"{hash_code}.{image.FileName.GetFileType()}";
            var filePath = FileUtils.GetLocalStorage() + "\\" + fileName;

            // 保存图片到本地
            FileUtils.SaveFile(image, filePath);

            var sysImage = new sys_file()
            {
                sys_fileId = id,
                name = fileName,
                file_path = filePath,
                file_type = fileType
            };
            new SysFileService().CreateData(sysImage);

            return new ImageInfo()
            {
                name = sysImage.name,
                path = $"{FileUtils.FILE_FOLDER}/{sysImage.name}"
            };
        }

    }
}