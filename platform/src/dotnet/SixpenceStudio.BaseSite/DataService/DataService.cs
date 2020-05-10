﻿using SixpenceStudio.BaseSite.DataService.Models;
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
        public ImageInfo UploadImage(HttpPostedFile image, string fileType, string objectId)
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
                hash_code = hash_code,
                file_path = filePath,
                file_type = fileType,
            };
            if (!string.IsNullOrEmpty(objectId))
            {
                sysImage.objectId = objectId;
            }

            new SysFileService().CreateData(sysImage);

            return new ImageInfo()
            {
                id = sysImage.sys_fileId,
                name = sysImage.name,
                path = $"{FileUtils.FILE_FOLDER}/{sysImage.name}"
            };
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns></returns>
        public string GetRandomImage()
        {
            var result = HttpUtils.Get("https://api.ixiaowai.cn/api/api.php?return=json");
            return result;
        }

    }
}